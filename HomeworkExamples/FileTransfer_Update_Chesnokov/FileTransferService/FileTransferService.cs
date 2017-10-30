using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.ServiceModel;

namespace FileTransferService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FileTransferService : IFileTransferService
    {
        private ConcurrentDictionary<string, Stream> _filesUploadStreams = new ConcurrentDictionary<string, Stream>();
        private ConcurrentDictionary<string, Stream> _filesDownloadStreams = new ConcurrentDictionary<string, Stream>();
        const int BufferSize = 10_485_760;

        public void Upload(FileDataUpload file)
        {
            try
            {
                Stream stream;
                bool fileIsFound = _filesUploadStreams.TryGetValue(file.FileId, out stream);
                
                if (fileIsFound)
                {
                    if(file.Data != null)
                        stream.Write(file.Data, 0, file.Data.Length);

                    if (file.IsLastPart)
                    {
                        stream.Close();

                        SaveFileInfoToDataBase(file);

                        _filesUploadStreams.TryRemove(file.FileId, out stream);
                    }
                }
                else
                {
                    string directoryPath = ConfigurationSettings.AppSettings["FileTransferPath"];

                    DirectoryInfo directoryInfo;
                    if (!Directory.Exists(directoryPath))
                        directoryInfo = Directory.CreateDirectory(directoryPath);
                    else
                        directoryInfo = new DirectoryInfo(directoryPath);

                    string filePath = Path.Combine(directoryInfo.FullName, file.FileId);

                    FileStream fsSource = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite | FileShare.Delete);
                    fsSource.Write(file.Data, 0, file.Data.Length);
                    
                    if (!file.IsLastPart)
                        _filesUploadStreams.TryAdd(file.FileId, fsSource);
                    else
                    {
                        fsSource.Close();
                        SaveFileInfoToDataBase(file);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void SaveFileInfoToDataBase(FileDataUpload file)
        {
            try
            {
                FileInfoEx fileInfoEx = new FileInfoEx
                {
                    FileNameOnServer = file.FileId,
                    OriginalFileName = file.FileName,
                    Author = file.Author,
                    DateUpload = file.DateUpload,
                    Description = file.Description
                };

                using (FileTransferContext context = new FileTransferContext())
                {
                    context.Files.Add(fileInfoEx);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public FileDataDownload Download(FileInfoEx fileInfoEx)
        {
            FileDataDownload fileDataDownload = null;

            try
            {
                Stream stream;
                bool fileIsFound = _filesDownloadStreams.TryGetValue(fileInfoEx.FileNameOnServer, out stream);
                
                if (fileIsFound)
                {
                    byte[] buffer = new byte[BufferSize];
                    int readBytes = stream.Read(buffer, 0, buffer.Length);

                    if (readBytes == 0)
                    {
                        stream.Close();
                        _filesDownloadStreams.TryRemove(fileInfoEx.FileNameOnServer, out stream);
                        
                        fileDataDownload = new FileDataDownload
                        {
                            IsLastPart = true
                        };
                    }
                    else
                    {
                        byte[] data = new byte[readBytes];
                        Array.Copy(buffer, data, readBytes);

                        fileDataDownload = new FileDataDownload
                        {
                            Data = data,
                            IsLastPart = false
                        };
                    }
                }
                else
                {
                    string directoryPath = ConfigurationSettings.AppSettings["FileTransferPath"];

                    DirectoryInfo directoryInfo;
                    if (!Directory.Exists(directoryPath))
                        directoryInfo = Directory.CreateDirectory(directoryPath);
                    else
                        directoryInfo = new DirectoryInfo(directoryPath);

                    string filePath = Path.Combine(directoryInfo.FullName, fileInfoEx.FileNameOnServer);

                    if (File.Exists(filePath))
                    {
                        FileStream fsSource = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        _filesDownloadStreams.TryAdd(fileInfoEx.FileNameOnServer, fsSource);
                        
                        byte[] buffer = new byte[BufferSize];
                        int readBytes = fsSource.Read(buffer, 0, buffer.Length);

                        if (readBytes == 0)
                        {
                            fsSource.Close();
                            _filesDownloadStreams.TryRemove(fileInfoEx.FileNameOnServer, out stream);
                            
                            fileDataDownload = new FileDataDownload
                            {
                                IsLastPart = true
                            };
                        }
                        else
                        {
                            byte[] data = new byte[readBytes];
                            Array.Copy(buffer, data, readBytes);

                            fileDataDownload = new FileDataDownload
                            {
                                Data = data,
                                IsLastPart = false
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return fileDataDownload;
        }

        public FileInfoEx[] GetListFiles(FileListType type, string value)
        {
            FileInfoEx[] filesEx = null;

            try
            {
                using (FileTransferContext context = new FileTransferContext())
                {
                    switch (type)
                    {
                        case FileListType.AllFiles:
                            filesEx = context.Files.ToArray();
                            break;
                        case FileListType.ByAuthor:
                            filesEx = context.Files.Where(f => f.Author.ToLower().Contains(value.ToLower())).ToArray();
                            break;
                        case FileListType.ByFileName:
                            filesEx = context.Files.Where(f => f.OriginalFileName.ToLower().Contains(value.ToLower())).ToArray();
                            break;
                        case FileListType.ByDateUpload:
                            DateTime date = DateTime.Parse(value);
                            filesEx = context.Files.Where(f => EntityFunctions.TruncateTime(f.DateUpload) >= EntityFunctions.TruncateTime(date)).ToArray();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return filesEx;
        }

        public void Remove(FileInfoEx fileInfoEx)
        {
            try
            {
                string directoryPath = ConfigurationSettings.AppSettings["FileTransferPath"];

                DirectoryInfo directoryInfo;
                if (!Directory.Exists(directoryPath))
                    directoryInfo = Directory.CreateDirectory(directoryPath);
                else
                    directoryInfo = new DirectoryInfo(directoryPath);

                string filePath = Path.Combine(directoryInfo.FullName, fileInfoEx.FileNameOnServer);

                File.Delete(filePath);

                using (FileTransferContext context = new FileTransferContext())
                {
                    FileInfoEx file =
                        context.Files.FirstOrDefault(f => f.FileNameOnServer.Equals(fileInfoEx.FileNameOnServer));

                    if (file != null)
                    {
                        context.Files.Remove(file);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
