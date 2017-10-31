using EmployeeManagerApp.EmployeeServiceReference;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeManagerApp
{
    /// <summary>   
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : MetroWindow
    {
        public Employee Employee { get; set; }
        public EmployeeWindow(Employee employee = null)
        {
            InitializeComponent();
            DateOfBirthDataPicker.DisplayDateStart = new DateTime(DateTime.Now.Year - 100, DateTime.Now.Month, DateTime.Now.Day);
            DateOfBirthDataPicker.DisplayDateEnd = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);
            CreateEmployee(employee);
            GenderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            GenderComboBox.SelectedValue = Employee.Gender;
            EmployeeTypeComboBox.ItemsSource = Enum.GetValues(typeof(EmployeeType));
            EmployeeTypeComboBox.SelectedValue = Employee.Type;
            DataContext = this;
        }

        private void CreateEmployee(Employee employee)
        {
            if (employee is null)
            {
                Employee = new Employee();
                Employee.DateOfBirth = (DateTime)DateOfBirthDataPicker.DisplayDateEnd;
                Employee.Type = EmployeeType.FullTimeEmployee;
            }
            else
            {
                if (employee is FullTimeEmployee)
                {
                    Employee = new FullTimeEmployee();
                    CopyBasePropertiesEmployee(employee);
                    Employee.Type = EmployeeType.FullTimeEmployee;

                    (Employee as FullTimeEmployee).MonthSallary = (employee as FullTimeEmployee).MonthSallary;
                    FullTimePay.Value = Convert.ToDouble((Employee as FullTimeEmployee).MonthSallary);
                }
                else if (employee is PartTimeEmployee)
                {
                    Employee = new PartTimeEmployee();
                    CopyBasePropertiesEmployee(employee);
                    Employee.Type = EmployeeType.PartTimeEmployee;
                    (Employee as PartTimeEmployee).HourlyPay = (employee as PartTimeEmployee).HourlyPay;
                    PartTimePay.Value = Convert.ToDouble((Employee as PartTimeEmployee).HourlyPay);
                    (Employee as PartTimeEmployee).HoursWorked = (employee as PartTimeEmployee).HoursWorked;
                    PartTimeHoursWorked.Value = Convert.ToDouble((Employee as PartTimeEmployee).HoursWorked);
                }
                TypeEmployeeChange();
            }
        }

        private void CopyBasePropertiesEmployee(Employee employee)
        {
            Employee.Id = employee.Id;
            Employee.Name = employee.Name;
            Employee.Gender = employee.Gender;
            Employee.DateOfBirth = employee.DateOfBirth;
            Employee.Type = employee.Type;
        }

        private void FullTimePay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (Employee is FullTimeEmployee)
                (Employee as FullTimeEmployee).MonthSallary = FullTimePay.Value is null ? 0 : Convert.ToDecimal((double)FullTimePay.Value);
        }

        private void PartTimePay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (Employee is PartTimeEmployee)
                (Employee as PartTimeEmployee).HourlyPay = PartTimePay.Value is null ? 0 : Convert.ToDecimal((double)PartTimePay.Value);
        }

        private void PartTimeHoursWorked_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

            if (Employee is PartTimeEmployee)
                (Employee as PartTimeEmployee).HoursWorked = PartTimeHoursWorked.Value is null ? 0 : Convert.ToInt32((double)PartTimeHoursWorked.Value);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(Employee.Name))
                DialogResult = true;
            else
                NameEmployee.BorderBrush = new SolidColorBrush(Colors.Red);
        }

        private void EmployeeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeTypeComboBox.SelectedValue != null)
            {
                Employee.Type = (EmployeeType)Enum.Parse(typeof(EmployeeType), EmployeeTypeComboBox.SelectedValue.ToString());
                if (Employee.Type == EmployeeType.FullTimeEmployee)
                {
                    FullTimeEmployee buff = new FullTimeEmployee();
                    buff.Id = Employee.Id;
                    buff.Name = Employee.Name;
                    buff.Gender = Employee.Gender;
                    buff.DateOfBirth = Employee.DateOfBirth;
                    buff.Type = Employee.Type;
                    if (FullTimePay.Value != null)
                        buff.MonthSallary = Convert.ToDecimal((double)FullTimePay.Value);
                    Employee = buff;
                }
                else if (Employee.Type == EmployeeType.PartTimeEmployee)
                {
                    PartTimeEmployee buff = new PartTimeEmployee();
                    buff.Id = Employee.Id;
                    buff.Name = Employee.Name;
                    buff.Gender = Employee.Gender;
                    buff.DateOfBirth = Employee.DateOfBirth;
                    buff.Type = Employee.Type;
                    if (PartTimePay.Value != null)
                        buff.HourlyPay = Convert.ToDecimal(PartTimePay.Value);
                    if (PartTimeHoursWorked.Value != null)
                        buff.HoursWorked = Convert.ToInt32(PartTimeHoursWorked.Value);
                    Employee = buff;
                }
                TypeEmployeeChange();
            }
        }

        private void TypeEmployeeChange()
        {
            if (Employee != null)
            {
                FullTimePay.Visibility = Employee is FullTimeEmployee ? Visibility.Visible : Visibility.Collapsed;
                PartTimeControls.Visibility = Employee is PartTimeEmployee ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
