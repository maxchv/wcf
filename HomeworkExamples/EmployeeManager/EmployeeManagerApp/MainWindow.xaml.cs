using EmployeeManagerApp.EmployeeServiceReference;
using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManagerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        ManagerServiceClient client;
        public MainWindow()
        {
            InitializeComponent();
            InitializeClient();
        }

        private void InitializeClient()
        {
            try
            {
                client = new ManagerServiceClient();
                //client.AddEmployee(new PartTimeEmployee() { Name = "Name1", DateOfBirth = DateTime.Now, Gender = Gender.Male, HourlyPay = 100m, HoursWorked = 20, Type = EmployeeType.PartTimeEmployee });
                //client.AddEmployee(new FullTimeEmployee() { Name = "Name1", DateOfBirth = DateTime.Now, Gender = Gender.Male, MonthSallary = 100m, Type = EmployeeType.FullTimeEmployee });

                GetAllEmployees();
                Connected();
            }
            catch (EndpointNotFoundException ex)
            {
                CantConnect();
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void GetAllEmployees()
        {
            var list = client.GetAllEmployees();
            PartTimeEmployeesList.ItemsSource = list.Where(e => e is PartTimeEmployee).ToList();
            FullTimeEmployeesList.ItemsSource = list.Where(e => e is FullTimeEmployee).ToList();

        }

        private void CantConnect()
        {
            ContentApplication.IsEnabled = false;
            ServiceConnect.Visibility = Visibility.Visible;
        }

        private void Connected()
        {
            ContentApplication.IsEnabled = true;
            ServiceConnect.Visibility = Visibility.Collapsed;
        }

        private void FullTimeRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = FullTimeEmployeesList.SelectedItem as FullTimeEmployee;
            RemoveEmployee(employee);
        }

        private void RemoveEmployee(Employee employee)
        {
            if (MessageBox.Show($"Вы действительно хотитие удалить {employee.Name}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    client.RemoveEmployee(FullTimeEmployeesList.SelectedItem as Employee);
                    GetAllEmployees();
                }
                catch (EndpointNotFoundException ex)
                {
                    CantConnect();
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FullTimeEditButton_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = (FullTimeEmployeesList.SelectedItem as Employee);
            CreateOrUpdateEmployee(selectedEmployee, true);
        }

        private void FullTimeAddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrUpdateEmployee(new FullTimeEmployee());
        }

        private void ServiceConnect_Click(object sender, RoutedEventArgs e)
        {
            InitializeClient();
        }

        private void PartTimeRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = PartTimeEmployeesList.SelectedItem as PartTimeEmployee;
            RemoveEmployee(employee);
        }

        private void PartTimeEditButton_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = (PartTimeEmployeesList.SelectedItem as Employee);
            CreateOrUpdateEmployee(selectedEmployee, isUpdate: true);
        }

        private void CreateOrUpdateEmployee(Employee employee, bool isUpdate = false)
        {
            EmployeeWindow wnd = new EmployeeWindow(employee);
            if ((bool)wnd.ShowDialog())
            {
                try
                {
                    if (isUpdate)
                    {
                        if (employee.Type == wnd.Employee.Type)
                            client.UpdateEmployee(wnd.Employee);
                        else
                        {
                            client.RemoveEmployee(employee);
                            client.AddEmployee(wnd.Employee);
                        }
                    }
                    else
                    {
                        client.AddEmployee(wnd.Employee);
                    }
                    GetAllEmployees();
                }
                catch (EndpointNotFoundException ex)
                {
                    CantConnect();
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void PartTimeAddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrUpdateEmployee(new PartTimeEmployee());
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetAllEmployees();
            }
            catch (EndpointNotFoundException ex)
            {
                CantConnect();
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
