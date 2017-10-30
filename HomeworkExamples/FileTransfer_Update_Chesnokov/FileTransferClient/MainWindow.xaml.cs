using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FileTransferClient.FileTransferServiceReference;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

namespace FileTransferClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Random _random;
        const int bufferSize = 10_485_760;

        public MainWindow()
        {
            InitializeComponent();

            _random = new Random();

            FileListTypeComboBox.ItemsSource = Enum.GetValues(typeof(FileListType));
        }

        private void BrowseButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                FileNameTextBox.Text = Path.GetFileName(dialog.FileName);
                FileNameTextBox.Tag = dialog.FileName;
            }
        }


        private async void UploadButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FileNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(FileNameTextBox.Text))
            {
                await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите имя файла!");
                return;
            }

            if (string.IsNullOrEmpty(AuthorTextBox.Text) ||
                string.IsNullOrWhiteSpace(AuthorTextBox.Text))
            {
                await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите автора!");
                return;
            }

            if (string.IsNullOrEmpty(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите автора!");
                return;
            }
            if (string.IsNullOrEmpty(FileNameTextBox.Tag.ToString()) ||
                string.IsNullOrWhiteSpace(FileNameTextBox.Tag.ToString()))
            {
                await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите файл!");
                return;
            }

            ProgressDialogController controller = await this.ShowProgressAsync("Загрузка файла", "Подождите идет загрузка файла на сервер...");
            controller.SetIndeterminate();

            try
            {
                FileTransferServiceClient client = new FileTransferServiceClient();

                using (FileStream stream = new FileStream(FileNameTextBox.Tag.ToString(), FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    byte[] buffer = new byte[bufferSize];
                    int readBytes;

                    string fileName = Path.GetFileNameWithoutExtension(FileNameTextBox.Tag.ToString());
                    string fileGuid = $"{Guid.NewGuid()}_#{_random.Next(0, 100_000)}";
                    string serverFileName = $"{fileName}_{fileGuid}_{Path.GetExtension(FileNameTextBox.Tag.ToString())}";

                    do
                    {
                        readBytes = stream.Read(buffer, 0, buffer.Length);

                        if (readBytes > 0)
                        {
                            byte[] data = new byte[readBytes];
                            Array.Copy(buffer, data, readBytes);

                            FileDataUpload dataUpload = new FileDataUpload
                            {
                                FileId = serverFileName,
                                FileName = FileNameTextBox.Text,
                                Author = AuthorTextBox.Text,
                                DateUpload = DateTime.Now,
                                Description = DescriptionTextBox.Text,
                                Data = data,
                                IsLastPart = false
                            };

                            await client.UploadAsync(dataUpload);
                        }
                        else
                        {
                            FileDataUpload dataUpload = new FileDataUpload
                            {
                                FileId = serverFileName,
                                FileName = FileNameTextBox.Text,
                                Author = AuthorTextBox.Text,
                                DateUpload = DateTime.Now,
                                Description = DescriptionTextBox.Text,
                                Data = null,
                                IsLastPart = true
                            };

                            await client.UploadAsync(dataUpload);
                        }
                    } while (readBytes > 0);
                }

                await this.ShowMessageAsync("Загрузка файла", "Файл был успешно загружен на сервер!");

                GetFilesList();
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка", exception.Message);
            }

            await controller.CloseAsync();
        }

        private void FileListTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FileListType type;
            Enum.TryParse(FileListTypeComboBox.SelectedValue.ToString(), out type);

            switch (type)
            {
                case FileListType.AllFiles:
                    ValueTextBox.Visibility = Visibility.Hidden;
                    break;
                case FileListType.ByAuthor:
                    DateUploadDatePicker.Visibility = Visibility.Hidden;
                    ValueTextBox.Visibility = Visibility.Visible;
                    break;
                case FileListType.ByFileName:
                    DateUploadDatePicker.Visibility = Visibility.Hidden;
                    ValueTextBox.Visibility = Visibility.Visible;
                    break;
                case FileListType.ByDateUpload:
                    DateUploadDatePicker.Visibility = Visibility.Visible;
                    ValueTextBox.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void GetFilesListButton_OnClick(object sender, RoutedEventArgs e)
        {
            GetFilesList();
        }

        private async void GetFilesList()
        {
            ProgressDialogController controller =
                await this.ShowProgressAsync("Загрузка списка файлов", "Подождите идет загрузка списка файлов...");
            controller.SetIndeterminate();

            try
            {
                FileListType type;
                Enum.TryParse(FileListTypeComboBox.SelectedValue.ToString(), out type);

                FileTransferServiceClient client = new FileTransferServiceClient();
                FileInfoEx[] files = null;

                switch (type)
                {
                    case FileListType.AllFiles:
                        files = await client.GetListFilesAsync(FileListType.AllFiles, "");
                        break;
                    case FileListType.ByAuthor:
                        if (string.IsNullOrEmpty(ValueTextBox.Text) ||
                            string.IsNullOrWhiteSpace(ValueTextBox.Text))
                        {
                            await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите автора!");
                            return;
                        }

                        files = await client.GetListFilesAsync(FileListType.ByAuthor, ValueTextBox.Text);
                        break;
                    case FileListType.ByFileName:
                        if (string.IsNullOrEmpty(ValueTextBox.Text) ||
                            string.IsNullOrWhiteSpace(ValueTextBox.Text))
                        {
                            await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите имя файла!");
                            return;
                        }

                        files = await client.GetListFilesAsync(FileListType.ByFileName, ValueTextBox.Text);
                        break;
                    case FileListType.ByDateUpload:
                        if (DateUploadDatePicker.SelectedDate == null)
                        {
                            await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите дату загрузки файла!");
                            return;
                        }

                        string date = DateUploadDatePicker.SelectedDate.Value.Date.ToShortDateString();
                        files = await client.GetListFilesAsync(FileListType.ByDateUpload, date);
                        break;
                }

                if (files != null)
                {
                    FilesDataGrid.ItemsSource = files;
                    if (files.Length <= 0)
                    {
                        if (type != FileListType.AllFiles)
                        {
                            await this.ShowMessageAsync("Результат поиска",
                                "К сожалению по указанным критериям ничего не было найдено!");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка", exception.Message);
            }

            await controller.CloseAsync();
        }

        private async void DownloadFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            FileInfoEx fileInfoEx = FilesDataGrid.SelectedItem as FileInfoEx;

            if (fileInfoEx != null)
            {
                ProgressDialogController controller = await this.ShowProgressAsync("Загрузка файла", "Подождите идет загрузка файла на компьютер...");
                controller.SetIndeterminate();

                try
                {
                    string directoryPath = ConfigurationSettings.AppSettings["DownloadFilesFolder"];

                    DirectoryInfo directoryInfo;
                    if (!Directory.Exists(directoryPath))
                        directoryInfo = Directory.CreateDirectory(directoryPath);
                    else
                        directoryInfo = new DirectoryInfo(directoryPath);

                    string filePath = Path.Combine(directoryInfo.FullName, fileInfoEx.OriginalFileName);

                    using (FileTransferServiceClient client = new FileTransferServiceClient())
                    using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        FileDataDownload download;

                        do
                        {
                            download = await client.DownloadAsync(fileInfoEx);

                            if (download.Data != null)
                            {
                                stream.Write(download.Data, 0, download.Data.Length);
                            }
                        } while (!download.IsLastPart);
                    }

                    await this.ShowMessageAsync("Загрузка файла", "Файл был успешно загружен на компьютер!");
                }
                catch (Exception exception)
                {
                    await this.ShowMessageAsync("Ошибка", exception.Message);
                }

                await controller.CloseAsync();
            }
        }


        private async void RemoveFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            ProgressDialogController controller = await this.ShowProgressAsync("Удаление файла", "Подождите идет удаление файла на сервере...");
            controller.SetIndeterminate();

            FileInfoEx fileInfoEx = FilesDataGrid.SelectedItem as FileInfoEx;

            if (fileInfoEx != null)
            {
                try
                {
                    using (FileTransferServiceClient client = new FileTransferServiceClient())
                    {
                        await client.RemoveAsync(fileInfoEx);
                    }

                    await this.ShowMessageAsync("Удаление файла", "Файл был успешно удален!");

                    GetFilesList();
                }
                catch (Exception exception)
                {
                    await this.ShowMessageAsync("Ошибка", exception.Message);
                }
            }

            await controller.CloseAsync();
        }
    }
}