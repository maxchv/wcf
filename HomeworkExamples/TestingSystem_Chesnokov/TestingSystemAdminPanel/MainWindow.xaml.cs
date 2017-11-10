using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using TestingSystemAdminPanel.TestingSystemServiceReference;

namespace TestingSystemAdminPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ITestingSystemServiceCallback
    {
        private InstanceContext _context;
        private TestingSystemServiceClient _client;

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
                await _client.GetListOfTestsAsync();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void BrowseFileTestButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Text File (*.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                FileTestPathTextBox.Text = dialog.FileName;
            }
        }

        private async void CreateNewTestButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TestNameTextBox.Text))
            {
                await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите название теста!");
                return;
            }

            if (CountQuestion.Value == null)
            {
                await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите количество вопросов которое будет выданно тестирующему!");
                return;
            }

            if (TimeTesting.Value == null)
            {
                await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите максимальное время прохождения теста (в мин.)!");
                return;
            }

            if (string.IsNullOrEmpty(FileTestPathTextBox.Text))
            {
                await this.ShowMessageAsync("Предупреждение", "Пожалуйста укажите файл в котором содержатся вопросы!");
                return;
            }

            try
            {
                Test test = new Test
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = TestNameTextBox.Text,
                    NumberOfQuestions = int.Parse(CountQuestion.Value.ToString()),
                    NumberMinutesToPassTest = int.Parse(TimeTesting.Value.ToString())
                };

                StringBuilder builder = new StringBuilder();

                using (FileStream stream = new FileStream(FileTestPathTextBox.Text, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(1251)))
                    {
                        builder.Append(reader.ReadToEnd());
                    }
                }

                List<Question> questionList = new List<Question>();
                Regex regex = new Regex(@"(?<QuestionId>\d+).\s?(?<QuestionText>.+)");

                if (regex.IsMatch(builder.ToString()))
                {
                    foreach (Match match in regex.Matches(builder.ToString()))
                    {
                        Question newQuestion = new Question
                        {
                            Id = Guid.NewGuid().ToString(),
                            Text = match.Groups["QuestionText"].Value.Replace("\r", "")
                        };

                        questionList.Add(newQuestion);
                    }
                }

                test.Questions = questionList.ToArray();

                if (questionList.Count < int.Parse(CountQuestion.Value.ToString()))
                {
                    await this.ShowMessageAsync("Предупреждение", 
                        "Количество выдаваемых вопросов тестирующему не может быть больше общего количества вопросов!");
                    return;
                }

                await _client.CreateNewTestAsync(test);
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (CreateNewTestButton_OnClick)", exception.Message);
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

        private async void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                if (_client?.State == CommunicationState.Opened)
                {
                    await _client.ClientDisconnectedAsync();
                    _client.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void SetTestResults(TestResult[] results)
        {
            try
            {
                if(results.Length == 0)
                    await this.ShowMessageAsync("Результат запроса", "У данного теста еще нет результатов!");

                TestResultsControl.ClearChildren();
                foreach (TestResult result in results)
                {
                    TestResultControl control = new TestResultControl(_client, result.Id);
                    control.SetUserInfo(result.User);
                    control.SetRating(result.Rating);

                    foreach (QuestionWithAnswer questionWithAnswer in result.Questions)
                    {
                        control.AddQuestionWithAnswer(new QuestionWithAnswerControl(questionWithAnswer.Text.Replace("\r", ""), questionWithAnswer.Answer));
                    }

                    TestResultsControl.AddNewResult(control);
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (SetTestResults)", exception.Message);
            }
        }

        private async void GetTestResutButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TestsListListBox.SelectedItem is TestInfo)
                {
                    TestInfo test = TestsListListBox.SelectedItem as TestInfo;

                    await _client.GetResultTestAsync(test.Id);
                }
                else
                {
                    await this.ShowMessageAsync("Предупреждение", "Пожалуйста выберите тест результаты которого Вы хотите загрузить!");
                }
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Ошибка (GetTestResutButton_OnClick)", exception.Message);
            }
        }

        public void SetQuestions(Question[] questions, string groupId, string subgroupId)
        {
            // ignored because this method for client application
        }

        public void ForciblyReturnTestResult()
        {
            // ignored because this method for client application
        }

        public void ResultOfUserRegistration(User user, string message)
        {
            // ignored because this method for client application
        }

        public void ResultEndTesting(bool result, string message)
        {
            // ignored because this method for client application
        }

        public void SetRemainingTime(long second)
        {
            // ignored because this method for client application
        }

        public void ResultOfUserSignIn(User user, string message)
        {
            // ignored because this method for client application
        }
    }
}