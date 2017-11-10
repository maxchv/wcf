using System.Windows;

namespace TestingSystemAdminPanel
{
    /// <summary>
    /// Interaction logic for TestResultsControl.xaml
    /// </summary>
    public partial class TestResultsControl
    {
        public TestResultsControl()
        {
            InitializeComponent();
        }

        public void AddNewResult(UIElement child)
        {
            QuestionsStackPanel.Children.Add(child);
        }

        public void ClearChildren()
        {
            QuestionsStackPanel.Children.Clear();
        }
    }
}