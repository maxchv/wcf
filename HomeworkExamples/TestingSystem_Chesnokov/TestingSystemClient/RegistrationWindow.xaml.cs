using System;
using System.ServiceModel;
using System.Windows;
using TestingSystemClient.TestingSystemServiceReference;

namespace TestingSystemClient
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow
    {
        private TestingSystemServiceClient _client;

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        public RegistrationWindow(TestingSystemServiceClient client)
        {
            InitializeComponent();

            _client = client;
        }

        private async void RegistrationNewUserButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Login.Text) || 
                    string.IsNullOrWhiteSpace(Login.Text))
                {
                    MessageBox.Show("Пожалуйста укажите логин!", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(Password.Password) ||
                    string.IsNullOrWhiteSpace(Password.Password))
                {
                    MessageBox.Show("Пожалуйста укажите пароль!", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(UserName.Text) ||
                    string.IsNullOrWhiteSpace(UserName.Text))
                {
                    MessageBox.Show("Пожалуйста укажите ФИО!", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(Group.Text) ||
                    string.IsNullOrWhiteSpace(Group.Text))
                {
                    MessageBox.Show("Пожалуйста укажите группу!", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_client.State == CommunicationState.Opened)
                {
                    bool isLoginFree = await _client.LoginIsFreeAsync(Login.Text);

                    if (isLoginFree)
                    {
                        User newUser = new User
                        {
                            Id = Guid.NewGuid().ToString(),
                            Login = Login.Text,
                            Password = Password.Password,
                            UserName = UserName.Text,
                            Group = Group.Text
                        };

                        bool isRegister = await _client.RegisterNewUserAsync(newUser);

                        if (isRegister)
                        {
                            MessageBox.Show("Вы были успешно зарегистрированны!", "Регистрация",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                            DialogResult = true;
                        }
                        else
                            MessageBox.Show("Вы не были зарегистрированны! Повторите попытку еще раз!", "Регистрация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Указанный логин занят! Укажите другой логин и повторите попытку!", "Регистрация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}