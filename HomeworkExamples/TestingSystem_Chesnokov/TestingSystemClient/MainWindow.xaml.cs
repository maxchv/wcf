using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls.Dialogs;
using TestingSystemClient.TestingSystemServiceReference;

namespace TestingSystemClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ITestingSystemServiceCallback
    {
        private InstanceContext _context;
        private TestingSystemServiceClient _client;
        private User _user;
        private List<QuestionControl> _questions = new List<QuestionControl>();
        private string _groupId;
        private string _subGroupId;
        private string _currentTestId;
        private bool _isTesting;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _context = new InstanceContext(this);
                _client = new TestingSystemServiceClient(_context);

                await _client.NewClientConnectedAsync();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private async void SigninToTestingSystemButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LoginTextBox.Text) ||
                    string.IsNullOrWhiteSpace(LoginTextBox.Text))
                {
                    await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите логин!");
                    return;
                }

                if (string.IsNullOrEmpty(PasswordPasswordBox.Password) ||
                    string.IsNullOrWhiteSpace(PasswordPasswordBox.Password))
                {
                    await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите пароль!");
                    return;
                }

                ProgressGrid.Visibility = Visibility.Visible;
                await _client.SignInAsync(LoginTextBox.Text, PasswordPasswordBox.Password);
            }
            catch (Exception exception)
            {
                ProgressGrid.Visibility = Visibility.Hidden;
                await this.ShowMessageAsync("Ошибка (SigninToTestingSystemButton_OnClick)", exception.Message);
            }
        }

        public async void ResultOfUserSignIn(User user, string message)
        {
            try
            {
                if (user == null)
                {
                    await this.ShowMessageAsync("Результат входа", message);
                }
                else
                {
                    _user = user;
                    UserPersonalInfoGroupBox.Visibility = Visibility.Collapsed;
                    ProgressGrid.Visibility = Visibility.Collapsed;
                    TestsGroupBox.IsEnabled = true;

                    await _client.GetListOfTestsAsync();
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (ResultOfUserRegistration)", exception.Message);
            }
            finally
            {
                ProgressGrid.Visibility = Visibility.Collapsed;
            }
        }

        public async void SetTestList(Dictionary<string, string> testsList)
        {
            try
            {
                List<TestInfo> testsInfo = new List<TestInfo>();

                foreach (KeyValuePair<string, string> pair in testsList)
                {
                    TestInfo newTest = new TestInfo
                    {
                        Id = pair.Key,
                        Name = pair.Value
                    };

                    testsInfo.Add(newTest);
                }

                TestsListListBox.ItemsSource = testsInfo;
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (SetTestList)", exception.Message);
            }
        }

        public async void SetQuestions(Question[] questions, string groupId, string subgroupId)
        {
            try
            {
                foreach (Question question in questions)
                {
                    QuestionControl newQuestionControl = new QuestionControl();
                    newQuestionControl.SetQuestionText(question.Text);
                    newQuestionControl.Question = question;

                    _questions.Add(newQuestionControl);

                    QuestionsControl.AddNewQuestion(newQuestionControl);
                }

                _groupId = groupId;
                _subGroupId = subgroupId;
                TestingGroupBox.IsEnabled = true;
                TakeToTestButton.IsEnabled = false;
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (SetQuestions)", exception.Message);
            }
        }

        public async void ForciblyReturnTestResult()
        {
            await CompleteTest();
        }

        private async void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                if (_client?.State == CommunicationState.Opened)
                {
                    if (_isTesting)
                    {
                        await CompleteTest();
                    }

                    await _client.ClientDisconnectedAsync();

                    _client.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void TakeToTestButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TestsListListBox.SelectedItem is TestInfo)
                {
                    TestInfo test = TestsListListBox.SelectedItem as TestInfo;

                    await _client.StartTestingAsync(_user, test.Id);
                    _currentTestId = test.Id;
                    CompleteTestButton.IsEnabled = true;
                    _isTesting = true;
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (TakeToTestButton_OnClick)", exception.Message);
            }
        }

        private async void CompleteTestButton_OnClick(object sender, RoutedEventArgs e)
        {
            await CompleteTest();
        }

        private async Task CompleteTest()
        {
            try
            {
                TestResult result = new TestResult();
                result.Id = Guid.NewGuid().ToString();
                result.User = _user;

                List<QuestionWithAnswer> answers = new List<QuestionWithAnswer>();
                foreach (QuestionControl element in _questions)
                {
                    QuestionWithAnswer answer = new QuestionWithAnswer
                    {
                        IdentifierdInTest = element.Question.Id,
                        Text = element.Question.Text,
                        Answer = element.GetAnswerToQuestion()
                    };

                    answers.Add(answer);
                }

                result.Questions = answers.ToArray();

                if (!string.IsNullOrEmpty(_currentTestId) &&
                    !string.IsNullOrWhiteSpace(_currentTestId))
                {
                    await _client.EndTestingAsync(result, _groupId, _subGroupId, _currentTestId);
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (CompleteTestButton_OnClick)", exception.Message);
            }
            finally
            {
                TakeToTestButton.IsEnabled = true;
            }
        }

        public async void ResultEndTesting(bool result, string message)
        {
            try
            {
                if (result)
                {
                    QuestionsControl.ClearChildren();
                    await this.ShowMessageAsync("Результат", message);
                }
                else
                {
                    await this.ShowMessageAsync("Результат", message);
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (ResultEndTesting)", exception.Message);
            }
            finally
            {
                _isTesting = false;
            }
        }

        public async void SetTestResults(TestResult[] results)
        {
            try
            {
                if (results.Length == 0)
                    await this.ShowMessageAsync("Результат запроса", "Вы еще не проходили этот тест!");
                else
                {
                    ResultsTestWindow window = new ResultsTestWindow(results);
                    window.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (SetTestResults)", exception.Message);
            }
        }

        public async void SetRemainingTime(long second)
        {
            try
            {
                long minutes = second > 60 ? second / 60 : 0;
                long seconds = second > 60 ? second - (minutes * 60) : second;

                MinutesTextBlock.Text = $"{minutes} мин.";
                SecondsTextBlock.Text = $"{seconds} сек.";

                if (minutes == 0 && seconds == 0)
                    CompleteTestButton.IsEnabled = false;

                if (minutes == 0 && seconds <= 30)
                {
                    if (seconds % 2 == 0)
                        TimeLeftBorder.Background = Brushes.Red;
                    else
                        TimeLeftBorder.Background = Brushes.Green;
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (ResultEndTesting)", exception.Message);
            }
        }

        private async void RegistrationButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistrationWindow window = new RegistrationWindow(_client);
                window.ShowDialog();
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (RegistrationButton_OnClick)", exception.Message);
            }
        }

        private async void GetResultsTestButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_isTesting)
                {
                    if (TestsListListBox.SelectedItem is TestInfo)
                    {
                        TestInfo test = TestsListListBox.SelectedItem as TestInfo;

                        await _client.GetResultsTestAsync(test.Id, _user.Id);
                    }
                    else
                    {
                        await this.ShowMessageAsync("Предупреждение", "Пожалуйста выберите тест результаты которого Вы хотите загрузить!");
                    }
                }
                else
                {
                    await this.ShowMessageAsync("Просмотр результатов", "Вы не можете просматривать результаты теста во время тестирования!");
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (GetResultsTestButton_OnClick)", exception.Message);
            }
        }
    }
}