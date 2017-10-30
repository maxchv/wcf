using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Calculator.CalculatorServiceReference;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICalculatorServiceCallback
    {
        private decimal _number1;
        private decimal _number2;
        private char _operation;

        public MainWindow()
        {
            InitializeComponent();

            var uri = new Uri("AnimatedDarkTheme.xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);

            Result1.Content = String.Empty;
            Result2.Content = String.Empty;

            _number1 = _number2 = 0;
            _operation = '\0';
        }

        private void ButtonNumber_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                Result1.Content = (Result1.Content != null) ? Result1.Content.ToString() + button.Content : button.Content;
            }
        }

        private void ButtonComma_OnClick(object sender, RoutedEventArgs e)
        {
            if (Result1.Content.ToString().IndexOf(',') == -1)
            {
                if (Result1.Content.ToString() == string.Empty || Result1.Content == null)
                {
                    Result1.Content = (Result1.Content != null) ? Result1.Content + "0," : "0,";
                }
                else
                {
                    Result1.Content = (Result1.Content != null) ? Result1.Content + "," : ",";
                }
            }
        }

        private void ButtonOperator_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                decimal.TryParse(Result1.Content.ToString(), out _number1);
                _operation = Convert.ToChar(button.Content.ToString());

                Result2.Content = _number1 + " " + _operation;
                Result1.Content = string.Empty;
            }
        }

        private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            Result1.Content = string.Empty;
            Result2.Content = string.Empty;

            _number1 = _number2 = 0;
            _operation = '\0';

            ButtonsEnabled(true);
        }

        private void ButtonPlusMinus_OnClick(object sender, RoutedEventArgs e)
        {
            decimal number;
            decimal.TryParse(Result1.Content.ToString(), out number);

            number *= -1;

            Result1.Content = number;
        }

        private void ButtonEqually_OnClick(object sender, RoutedEventArgs e)
        {
            if (Result1.Content.ToString() == string.Empty || Result1.Content == null)
            {
                _number2 = _number1;
            }
            else
            {
                decimal.TryParse(Result1.Content.ToString(), out _number2);
            }

            Сalculation();
        }

        private void Сalculation()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            CalculatorServiceClient client = new CalculatorServiceClient(instanceContext);

            try
            {
                switch (_operation)
                {
                    case '+':
                        client.Sum(_number1, _number2);
                        break;
                    case '-':
                        client.Sub(_number1, _number2);
                        break;
                    case '*':
                        client.Mult(_number1, _number2);
                        break;
                    case '/':
                        if(_number2 != 0)
                            client.Div(_number1, _number2);
                        else
                            throw new DivideByZeroException();
                        break;
                }
            }
            //catch (FaultException<ExceptionExType> exception)
            //{
            //    ButtonsEnabled(false);
            //    ExceptionExType exceptionExType = exception.Detail;
            //    MessageBox.Show($"Method name: {exceptionExType.MethodName}\r\n" +
            //                    $"Number line: {exceptionExType.Line}\r\n" + 
            //                    $"Description: {exceptionExType.Description}\r\n" +
            //                    $"Message: {exceptionExType.Message}\r\n",
            //                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            catch (DivideByZeroException)
            {
                Result2.Content = string.Empty;
                Result1.Content = "∞";
                ButtonsEnabled(false);
            }
            catch (Exception)
            {
                ButtonsEnabled(false);
            }
        }
        public void Calculation(decimal resultCalculation)
        {
            _number1 = Math.Round(resultCalculation, 16);
            Result2.Content = string.Empty;
            Result1.Content = _number1;
        }

        private void ButtonsEnabled(bool isEnabled)
        {
            Button[] buttons = { ButtonZero, ButtonOne, ButtonTwo, ButtonThree, ButtonFour,
                ButtonFive, ButtonSix, ButtonSeven, ButtonEight, ButtonNine, ButtonComma, ButtonPlus,
                ButtonMinus, ButtonMultiply, ButtonDevide, ButtonEqually, ButtonPlusMinus };

            foreach (Button button in buttons)
            {
                button.IsEnabled = isEnabled;
            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                var uri = new Uri((sender as MenuItem).Tag.ToString(), UriKind.Relative);
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainGrid.Focus();
        }
    }
}