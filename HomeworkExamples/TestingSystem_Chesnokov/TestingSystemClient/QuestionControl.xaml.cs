using TestingSystemClient.TestingSystemServiceReference;

namespace TestingSystemClient
{
    /// <summary>
    /// Interaction logic for QuestionControl.xaml
    /// </summary>
    public partial class QuestionControl
    {
        public Question Question { get; set; }

        public QuestionControl()
        {
            InitializeComponent();
        }

        public void SetQuestionText(string text)
        {
            QuestionText.Text = text;
        }

        public string GetAnswerToQuestion()
        {
            return AnswerToQuestionTextBox.Text;
        }
    }
}