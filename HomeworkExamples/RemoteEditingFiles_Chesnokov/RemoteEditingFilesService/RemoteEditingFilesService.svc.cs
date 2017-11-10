using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace RemoteEditingFilesService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                         ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RemoteEditingFilesService : IRemoteEditingFilesService
    {
        private ConcurrentDictionary<string, IServiceCallback> _callbackClientList = new ConcurrentDictionary<string, IServiceCallback>();
        private ConcurrentDictionary<string, List<IServiceCallback>> _editableFiles = new ConcurrentDictionary<string, List<IServiceCallback>>();
        //private string _directoryPath = $@"C:\Users\{Environment.UserName}\AppData\Local\Temp\RemoteEditingFiles_Temp";
        private object locker = new object();

        private IServiceCallback Callback
        {
            get
            {
                IServiceCallback callback = OperationContext.Current?.GetCallbackChannel<IServiceCallback>();
                return callback;
            }
        }

        private string GetDirectory()
        {
            try
            {
                string pathRoot = Path.GetPathRoot(Environment.SystemDirectory);
                string directoryPath =
                    $@"{pathRoot}Users\{Environment.UserName}\AppData\Local\Temp\RemoteEditingFiles_Temp";

                DirectoryInfo directoryInfo;
                if (!Directory.Exists(directoryPath))
                {
                    directoryInfo = Directory.CreateDirectory(directoryPath);
                }
                else
                    directoryInfo = new DirectoryInfo(directoryPath);

                return directoryInfo.FullName;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void NewClientConnects()
        {
            string id = Guid.NewGuid().ToString();

            IServiceCallback callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
            bool isAdding = _callbackClientList.TryAdd(id, callback);

            if (isAdding)
            {
                callback.SetUserId(id);
            }
        }

        public void ClientDisconnected(string id)
        {
            IServiceCallback callback;
            _callbackClientList.TryRemove(id, out callback);
        }

        public void GetFilesList()
        {
            SetFilesNameList(new[] { Callback });
        }

        private void SetFilesNameList(IServiceCallback[] callbackClients)
        {
            try
            {
                string dir = GetDirectory();

                if (!string.IsNullOrEmpty(dir))
                {
                    string[] filesNameStrings = Directory.GetFiles(dir, "*.txt");
                    string[] filesName = new string[filesNameStrings.Length];

                    for (int i = 0; i < filesNameStrings.Length; i++)
                    {
                        filesName[i] = Path.GetFileName(filesNameStrings[i]);
                    }

                    foreach (IServiceCallback callback in callbackClients)
                    {
                        callback.SendFilesList(filesName);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public async void CreateNewFile(string fileName, string id, string initData)
        {
            IServiceCallback callback;
            _callbackClientList.TryGetValue(id, out callback);

            if (callback != null)
            {
                try
                {
                    string dir = GetDirectory();

                    if (!string.IsNullOrEmpty(dir))
                    {
                        string path = Path.Combine(dir, fileName);

                        if (File.Exists(path))
                            callback.SendResultCreateNewFile("Файл с таким именем уже существует! Укажите другое имя файла!");
                        else
                        {
                            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write,
                                FileShare.ReadWrite | FileShare.Delete))
                            {
                                using (StreamWriter writer = new StreamWriter(stream))
                                {
                                    await writer.WriteAsync(initData);
                                }
                            }

                            callback.SendResultCreateNewFile("Файл с указанным имененем был успешно создан!");

                            SetFilesNameList(_callbackClientList.Values.ToArray());
                        }
                    }
                    else
                    {
                        callback.SendResultCreateNewFile("Файл не был создан!");
                    }
                }
                catch (Exception e)
                {
                    callback.SendResultCreateNewFile(e.Message);
                }
            }
        }

        public void RemoveFile(string fileName, string id)
        {
            IServiceCallback callback;
            _callbackClientList.TryGetValue(id, out callback);

            if (callback != null)
            {
                try
                {
                    string dir = GetDirectory();

                    if (!string.IsNullOrEmpty(dir))
                    {
                        string path = Path.Combine(dir, fileName);

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            Callback.SendResultRemoveFile("Указанный файл был успешно удален!");

                            SetFilesNameList(_callbackClientList.Values.ToArray());
                        }
                        else
                        {
                            Callback.SendResultRemoveFile("Указанный файл не найден!");
                        }
                    }
                }
                catch (Exception exception)
                {
                    Callback.SendResultRemoveFile(exception.Message);
                }
            }
        }

        public void StartEditFile(string fileName, string id)
        {
            try
            {
                string dir = GetDirectory();

                if (!string.IsNullOrEmpty(dir))
                {
                    string path = Path.Combine(dir, fileName);

                    if (File.Exists(path))
                    {
                        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                        {
                            using (StreamReader reader = new StreamReader(fileStream, Encoding.GetEncoding(1251)))
                            {
                                string data = reader.ReadToEnd();

                                List<IServiceCallback> callbacks;
                                bool isFound = _editableFiles.TryGetValue(fileName, out callbacks);

                                if (isFound)
                                {
                                    if (callbacks is null)
                                        callbacks = new List<IServiceCallback>();

                                    IServiceCallback callback;
                                    _callbackClientList.TryGetValue(id, out callback);

                                    if (callback != null)
                                    {
                                        callbacks.Add(Callback);
                                        _editableFiles.TryUpdate(fileName, callbacks, callbacks);
                                        
                                        callback.SendDataFile(data);
                                    }
                                }
                                else
                                {
                                    callbacks = new List<IServiceCallback>();

                                    IServiceCallback callback;
                                    _callbackClientList.TryGetValue(id, out callback);

                                    if (callback != null)
                                    {
                                        callbacks.Add(Callback);
                                        _editableFiles.TryAdd(fileName, callbacks);

                                        callback.SendDataFile(data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void EndEditFile(string fileName, string id)
        {
            try
            {
                List<IServiceCallback> callbacks;
                bool isFound = _editableFiles.TryGetValue(fileName, out callbacks);

                if (isFound)
                {
                    if (callbacks != null)
                    {
                        IServiceCallback callback;
                        _callbackClientList.TryGetValue(id, out callback);

                        if (callback != null)
                        {
                            callbacks.Remove(callback);
                            _editableFiles.TryUpdate(fileName, callbacks, callbacks);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void UpdateFileData(string fileName, string newData, string id)
        {
            lock (locker)
            {
                try
                {
                    string dir = GetDirectory();

                    if (!string.IsNullOrEmpty(dir))
                    {
                        string path = Path.Combine(dir, fileName);

                        if (File.Exists(path))
                        {
                            using (StreamWriter writer = new StreamWriter(path, false))
                            {
                                writer.Write(newData);
                            }

                            List<IServiceCallback> callbacks = null;
                            bool isFound = _editableFiles.TryGetValue(fileName, out callbacks);

                            if (isFound)
                            {
                                if (callbacks != null)
                                {
                                    IServiceCallback thisUsercallback;
                                    _callbackClientList.TryGetValue(id, out thisUsercallback);

                                    if (thisUsercallback != null)
                                    {
                                        foreach (IServiceCallback callback in callbacks)
                                        {
                                            if ((callback as IChannel).State == CommunicationState.Opened)
                                            {
                                                if (callback != thisUsercallback)
                                                    callback.SendDataFile(newData);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
        }
    }
}
