using System.Windows;

namespace TestingSystemClient
{
    /// <summary>
    /// Interaction logic for QuestionsControl.xaml
    /// </summary>
    public partial class QuestionsControl
    {
        public QuestionsControl()
        {
            InitializeComponent();
        }

        public void AddNewQuestion(UIElement child)
        {
            QuestionsStackPanel.Children.Add(child);
        }

        public void ClearChildren()
        {
            QuestionsStackPanel.Children.Clear();
        }
    }
}