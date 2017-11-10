using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using RemoteEditingFilesClient.RemoteEditingFilesServiceReference;

namespace RemoteEditingFilesClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IRemoteEditingFilesServiceCallback
    {
        private InstanceContext _context;
        private RemoteEditingFilesServiceClient _client;
        private string _selectedFileName = null;
        private bool isTextChange = false;
        private string _id = String.Empty;
        private bool _isThisClientRemoveFile = false;
        private string _savedData = String.Empty;
        private bool _savedFile = false;
        private int _count = 0;
        private string _savedFileName = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _context = new InstanceContext(this);
                _client = new RemoteEditingFilesServiceClient(_context);

                await _client.NewClientConnectsAsync();

                await _client.GetFilesListAsync();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка при запуске проложения",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                this.Close();
            }
        }

        public async void SendFilesList(string[] filesName)
        {
            try
            {
                if (!string.IsNullOrEmpty(_selectedFileName))
                {
                    string r = filesName.FirstOrDefault(n => n.Equals(_selectedFileName));

                    if (string.IsNullOrEmpty(r) && !_isThisClientRemoveFile && _count > filesName.Length)
                    {
                        do
                        {
                            MetroDialogSettings settings = new MetroDialogSettings();
                            settings.AffirmativeButtonText = "Сохранить";
                            settings.NegativeButtonText = "Не сохранять";

                            string fileName = await this.ShowInputAsync("Сохранение файла",
                                "Редактируемый файл был удален на сервере! Сохранить текст в другой файл?", settings);

                            if (fileName is null)
                            {
                                FileTextTextBox.Text = String.Empty;
                                _savedFile = false;
                                break;
                            }
                            else if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
                                await this.ShowMessageAsync("Предупреждение", "Имя файла не может быть пустым!");
                            else
                            {
                                if (string.IsNullOrEmpty(Path.GetExtension(fileName)))
                                    fileName = fileName + ".txt";

                                _savedData = FileTextTextBox.Text;
                                _savedFile = true;
                                _savedFileName = fileName;
                                await _client.CreateNewFileAsync(fileName, _id, _savedData);
                                break;
                            }
                        } while (true);
                    }
                }

                FilesNameListBox.ItemsSource = filesName;
                _count = filesName.Length;
            }
            catch (Exception e)
            {
                await this.ShowMessageAsync("Ошибка", e.Message);
            }

        }

        private async void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                if (_selectedFileName != null)
                {
                    await _client.EndEditFileAsync(_selectedFileName, _id);
                }

                await _client.ClientDisconnectedAsync(_id);
                _client.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public async void SendResultCreateNewFile(string message)
        {
            if (message.Contains("Файл с таким именем уже существует! Укажите другое имя файла!"))
            {
                await this.ShowMessageAsync("Результат добавления нового файла", message);
                await AddNewFileToServer();
            }
            else
            {
                if (_savedFile)
                {
                    FilesNameListBox.SelectedItem = _savedFileName;
                    _savedFile = false;
                }
                else
                {
                    await this.ShowMessageAsync("Результат добавления нового файла", message);
                }
            }
        }

        public async void SendResultRemoveFile(string message)
        {
            await this.ShowMessageAsync("Результат удаления выбраного файла", message);
            isTextChange = false;
            FileTextTextBox.Text = String.Empty;
        }

        public async void SendDataFile(string text)
        {
            try
            {
                isTextChange = false;
                FileTextTextBox.Text = text;
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка", exception.Message);
            }
        }

        public void SetUserId(string id)
        {
            _id = id;
        }

        private async void AddNewFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            await AddNewFileToServer();
        }

        private async Task AddNewFileToServer()
        {
            try
            {
                do
                {
                    MetroDialogSettings settings = new MetroDialogSettings();
                    settings.AffirmativeButtonText = "Добавить";
                    settings.NegativeButtonText = "Отмена";

                    string fileName = await this.ShowInputAsync("Укажите имя нового файла", "", settings);

                    if (fileName is null)
                        break;
                    else if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
                        await this.ShowMessageAsync("Предупреждение", "Имя файла не может быть пустым!");
                    else
                    {
                        if (string.IsNullOrEmpty(Path.GetExtension(fileName)))
                            fileName = fileName + ".txt";

                        _savedFileName = fileName;

                        await _client.CreateNewFileAsync(fileName, _id, _savedData);
                        break;
                    }
                } while (true);
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка", exception.Message);
            }
        }

        private async void RemoveFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FilesNameListBox.SelectedValue != null)
                {
                    _isThisClientRemoveFile = true;
                    string fileName = FilesNameListBox.SelectedValue.ToString();
                    await _client.RemoveFileAsync(fileName, _id);
                }
                else
                    await this.ShowMessageAsync("Предупреждение", "Пожалуйста выберите файл который хотите удалить!");
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка", exception.Message);
            }
        }

        private async void FilesNameListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (FilesNameListBox.SelectedValue != null)
                {
                    if (_selectedFileName != null)
                    {
                        await _client.EndEditFileAsync(_selectedFileName, _id);
                    }

                    string fileName = FilesNameListBox.SelectedValue.ToString();
                    _selectedFileName = fileName;
                    await _client.StartEditFileAsync(fileName, _id);
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка", exception.Message);
            }

            _isThisClientRemoveFile = false;
        }

        private async void FileTextTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(isTextChange)
                    await _client.UpdateFileDataAsync(_selectedFileName, FileTextTextBox.Text, _id);
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка", exception.Message);
            }

            isTextChange = true;
        }
    }
}