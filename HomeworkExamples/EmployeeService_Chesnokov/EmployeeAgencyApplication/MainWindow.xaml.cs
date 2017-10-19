using System;
using System.Windows;
using System.Windows.Controls;
using EmployeeAgencyApplication.EmployeesServiceReference;
using MahApps.Metro.Controls.Dialogs;

namespace EmployeeAgencyApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        enum EmployeeTypeView
        {
            Total,
            FullTimeEmployee,
            PartTimeEmployee,
        }

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                EmployeeGenderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
                EmployeeTypeComboBox.ItemsSource = Enum.GetValues(typeof(EmployeeType));
                EmployeeTypeViewCombobox.ItemsSource = Enum.GetNames(typeof(EmployeeTypeView));
            }
            catch
            {
                // ignored
            }
        }

        private void SetEmployeeList()
        {
            try
            {
                EmployeeTypeView view;
                Enum.TryParse(EmployeeTypeViewCombobox.SelectedValue.ToString(), out view);

                if (view == EmployeeTypeView.FullTimeEmployee)
                {
                    EmployeeServiceClient client = new EmployeeServiceClient();
                    Employee[] employees = client.GetEmployees(EmployeeType.FullTimeEmployee);
                    EmployeesDataGrid.ItemsSource = employees;
                }
                else if (view == EmployeeTypeView.PartTimeEmployee)
                {
                    EmployeeServiceClient client = new EmployeeServiceClient();
                    Employee[] employees = client.GetEmployees(EmployeeType.PartTimeEmployee);
                    EmployeesDataGrid.ItemsSource = employees;
                }
                else if (view == EmployeeTypeView.Total)
                {
                    EmployeeServiceClient client = new EmployeeServiceClient();
                    Employee[] employeesFull = client.GetEmployees(EmployeeType.FullTimeEmployee);
                    Employee[] employeesPart = client.GetEmployees(EmployeeType.PartTimeEmployee);

                    Employee[] employees = new Employee[employeesFull.Length + employeesPart.Length];

                    Array.Copy(employeesFull, 0, employees, 0, employeesFull.Length);
                    Array.Copy(employeesPart, 0, employees, employeesFull.Length, employeesPart.Length);

                    EmployeesDataGrid.ItemsSource = employees;
                }
            }
            catch (Exception exception)
            {                
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }

        private async void AddNewEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            ProgressDialogController controller = await this.ShowProgressAsync("Пожалуйста подождите", "Идет добавление нового сотрудника...");
            controller.SetIndeterminate();

            try
            {
                EmployeeServiceClient client = new EmployeeServiceClient();

                Employee employee = GetEmployee();
                client.AddEmployee(employee);

                SetEmployeeList();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await controller.CloseAsync();
        }

        private Employee GetEmployee()
        {
            Employee returnEmployee = null;

            if (string.IsNullOrEmpty(EmployeeNameTextBox.Text))
                throw new Exception("Пожалуйста укажите ФИО сотрудника!");

            if (EmployeeDateOfBirth.SelectedDate == null)
                throw new Exception("Пожалуйста укажите дату рождения сотрудника!");

            if (EmployeeGenderComboBox.SelectedIndex == -1)
                throw new Exception("Пожалуйста укажите пол сотрудника!");

            if (EmployeeTypeComboBox.SelectedIndex == -1)
                throw new Exception("Пожалуйста укажите тип сотрудника!");

            EmployeeType selectedType;
            Enum.TryParse(EmployeeTypeComboBox.SelectedValue.ToString(), out selectedType);

            if (selectedType == EmployeeType.FullTimeEmployee)
            {
                if (MonthSallaryNumericUpDown.Value == null)
                    throw new Exception("Пожалуйста укажите месячный оклад!");

                FullTimeEmployee employee = new FullTimeEmployee();
                SetValuesEmployee(employee, selectedType);

                employee.MonthSallary = Convert.ToDecimal(MonthSallaryNumericUpDown.Value);

                returnEmployee = employee;
            }
            else if (selectedType == EmployeeType.PartTimeEmployee)
            {
                if (HourlyPayNumericUpDown.Value == null)
                    throw new Exception("Пожалуйста укажите часовую оплату!");

                if (HoursWorkedNumericUpDown.Value == null)
                    throw new Exception("Пожалуйста укажите кол-во отработанных часов!");

                PartTimeEmployee employee = new PartTimeEmployee();
                SetValuesEmployee(employee, selectedType);

                employee.HourlyPay = Convert.ToDecimal(HourlyPayNumericUpDown.Value);
                employee.HoursWorked = Convert.ToInt32(HoursWorkedNumericUpDown.Value);

                returnEmployee = employee;
            }

            return returnEmployee;
        }

        private void SetValuesEmployee(Employee employee, EmployeeType employeeType)
        {
            try
            {
                employee.Name = EmployeeNameTextBox.Text;
                employee.DateOfBirth = EmployeeDateOfBirth.SelectedDate.Value;

                Gender gender;
                Enum.TryParse(EmployeeGenderComboBox.SelectedValue.ToString(), out gender);

                employee.Gender = gender;
                employee.EmployeeType = employeeType;
            }
            catch 
            {
                // ignored
            }
        }

        private async void EditEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            ProgressDialogController controller = await this.ShowProgressAsync("Пожалуйста подождите", "Идет измененение данных сотрудника...");
            controller.SetIndeterminate();

            try
            {
                Employee selectEmployee = EmployeesDataGrid.SelectedItem as Employee;

                if (selectEmployee == null)
                    throw new Exception("Пожалуйста выберите сотрудника которого хотите удалить!");

                EmployeeServiceClient client = new EmployeeServiceClient();
                Employee newEmployeeData = GetEmployee();

                bool result = client.EditEmployee(selectEmployee.Id, newEmployeeData);

                if (result)
                    await this.ShowMessageAsync("", "Данные сотрудника были успешно измененены!");                    
                else
                    await this.ShowMessageAsync("", "Данные сотрудника не были измененены!");

                SetEmployeeList();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await controller.CloseAsync();
        }

        private async void RemoveEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            ProgressDialogController controller = await this.ShowProgressAsync("Пожалуйста подождите", "Идет удаление данных сотрудника...");
            controller.SetIndeterminate();

            try
            {
                Employee employee = EmployeesDataGrid.SelectedItem as Employee;

                if (employee == null)
                    throw new Exception("Пожалуйста выберите сотрудника которого хотите удалить!");

                EmployeeServiceClient client = new EmployeeServiceClient();
                bool result = client.RemoveEmployee(employee.Id);

                if (result)
                    await this.ShowMessageAsync("", "Сотрудник был успешно удален!");
                else
                    await this.ShowMessageAsync("", "Сотрудник не был удален!");

                SetEmployeeList();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await controller.CloseAsync();
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            EmployeeNameTextBox.Text = String.Empty;
            EmployeesDataGrid.SelectedIndex = -1;
        }

        private void EmployeeTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (EmployeeTypeComboBox.SelectedValue == null)
                    return;

                EmployeeType selectedType;
                Enum.TryParse(EmployeeTypeComboBox.SelectedValue.ToString(), out selectedType);

                if (selectedType == EmployeeType.FullTimeEmployee)
                {
                    HourlyPayNumericUpDown.IsEnabled = false;
                    HourlyPayNumericUpDown.Value = null;
                    HoursWorkedNumericUpDown.IsEnabled = false;
                    HoursWorkedNumericUpDown.Value = null;

                    MonthSallaryNumericUpDown.IsEnabled = true;
                    MonthSallaryLabel.IsEnabled = true;
                }
                else if (selectedType == EmployeeType.PartTimeEmployee)
                {
                    HourlyPayNumericUpDown.IsEnabled = true;
                    HoursWorkedNumericUpDown.IsEnabled = true;

                    MonthSallaryNumericUpDown.IsEnabled = false;
                    MonthSallaryLabel.IsEnabled = false;
                    MonthSallaryNumericUpDown.Value = null;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EmployeeTypeViewCombobox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetEmployeeList();
        }

        private void EmployeesDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Employee employee = EmployeesDataGrid.SelectedItem as Employee;

                if (employee == null)
                    return;

                EmployeeNameTextBox.Text = employee.Name;
                EmployeeDateOfBirth.SelectedDate = employee.DateOfBirth;
                EmployeeGenderComboBox.SelectedItem = employee.Gender;
                EmployeeTypeComboBox.SelectedItem = employee.EmployeeType;

                if (employee.EmployeeType == EmployeeType.FullTimeEmployee &&
                    employee is FullTimeEmployee)
                {
                    MonthSallaryNumericUpDown.Value = Convert.ToDouble((employee as FullTimeEmployee).MonthSallary);
                    HourlyPayNumericUpDown.Value = null;
                    HoursWorkedNumericUpDown.Value = null;
                }
                else if (employee.EmployeeType == EmployeeType.PartTimeEmployee &&
                         employee is PartTimeEmployee)
                {
                    HourlyPayNumericUpDown.Value = Convert.ToDouble((employee as PartTimeEmployee).HourlyPay);
                    HoursWorkedNumericUpDown.Value = Convert.ToDouble((employee as PartTimeEmployee).HoursWorked);
                    MonthSallaryNumericUpDown.Value = null;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}