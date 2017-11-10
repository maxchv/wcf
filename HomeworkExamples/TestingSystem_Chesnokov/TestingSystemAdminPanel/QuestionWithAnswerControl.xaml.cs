namespace TestingSystemAdminPanel
{
    /// <summary>
    /// Interaction logic for QuestionWithAnswerControl.xaml
    /// </summary>
    public partial class QuestionWithAnswerControl
    {
        public QuestionWithAnswerControl()
        {
            InitializeComponent();
        }

        public QuestionWithAnswerControl(string question, string answer)
        {
            InitializeComponent();

            QuestionText.Text = question;
            AnswerText.Text = answer;
        }
    }
}