using System;
using System.Windows;
using TestingSystemClient.TestingSystemServiceReference;

namespace TestingSystemClient
{
    /// <summary>
    /// Interaction logic for ResultsTestWindow.xaml
    /// </summary>
    public partial class ResultsTestWindow
    {
        public ResultsTestWindow(TestResult[] results)
        {
            InitializeComponent();

            try
            {
                foreach (TestResult result in results)
                {
                    TestResultControl control = new TestResultControl();
                    control.SetUserInfo(result.User);
                    control.SetRating(result.Rating);

                    foreach (QuestionWithAnswer questionWithAnswer in result.Questions)
                    {
                        control.AddQuestionWithAnswer(new QuestionWithAnswerControl(questionWithAnswer.Text.Replace("\r", ""), questionWithAnswer.Answer));
                    }

                    ResultsStackPanel.Children.Add(control);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}