using System;
using System.ServiceModel;
using System.Windows;
using TestingSystemAdminPanel.TestingSystemServiceReference;

namespace TestingSystemAdminPanel
{
    /// <summary>
    /// Interaction logic for TestResultControl.xaml
    /// </summary>
    public partial class TestResultControl
    {
        private TestingSystemServiceClient _client;
        private string _resultId;

        public TestResultControl(TestingSystemServiceClient client, string resultId)
        {
            InitializeComponent();

            _client = client;
            _resultId = resultId;
        }

        public void SetUserInfo(User user)
        {
            UserInfoGroupBox.DataContext = user;
        }

        public void AddQuestionWithAnswer(UIElement element)
        {
            QuestionsStackPanel.Children.Add(element);
        }

        public void SetRating(int rating)
        {
            Rating.Value = rating;
        }

        private async void Rating_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try
            {
                if (_client?.State == CommunicationState.Opened)
                {
                    bool isChanged = await _client.ChangeRatingAsync(_resultId, Convert.ToInt32(Rating.Value));

                    if (isChanged)
                        MessageBox.Show("Оценка была успешно изменена!", "Изменение оценки", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    else
                        MessageBox.Show("Оценка не была изменена!", "Изменение оценки", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}