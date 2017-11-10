using System.Windows;
using TestingSystemClient.TestingSystemServiceReference;

namespace TestingSystemClient
{
    /// <summary>
    /// Interaction logic for TestResultControl.xaml
    /// </summary>
    public partial class TestResultControl
    {
        public TestResultControl()
        {
            InitializeComponent();
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
            Result.Header = $"Оценка {rating} балов";
        }
    }
}