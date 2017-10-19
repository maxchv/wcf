using MahApps.Metro.Controls;
using RentAutoClient.RentServiceReference;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RentAutoClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            GetAllCar();
        }

        private async void GetAllCar()
        {
            try
            {
                var service = new RentServiceClient();
                CarsList.ItemsSource = await service.GetAllCarsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CarsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarsList.SelectedItem != null)
            {
                GetRent(CarsList.SelectedItem?.ToString(), DateStartRent.SelectedDate, DateEndRent.SelectedDate);
                RemoveItem.Visibility = Visibility.Visible;
            }
            else
                RemoveItem.Visibility = Visibility.Collapsed;
        }

        private void GetRent(string brand, DateTime? start, DateTime? end)
        {
            if (!String.IsNullOrWhiteSpace(brand) && start != null && end != null)
            {
                try
                {
                    var service = new RentServiceClient();
                    RentSale.Text = service.GetRent((DateTime)start, (DateTime)end, brand) + "$";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            GetRent(CarsList.SelectedItem?.ToString(), DateStartRent.SelectedDate, DateEndRent.SelectedDate);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CarWindow wnd = new CarWindow();
            if ((bool)wnd.ShowDialog())
            {
                AddCar(wnd.BrandVal, wnd.RentVal);
                GetAllCar();
            }
        }

        private void AddCar(string brand, decimal rent)
        {
            try
            {
                var service = new RentServiceClient();
                service.AddCar(brand, rent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы действительно хотите удалить {CarsList.SelectedItem.ToString()}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                RemoveCar(CarsList.SelectedItem.ToString());

            GetAllCar();
        }

        private void RemoveCar(string brand)
        {
            try
            {
                var service = new RentServiceClient();
                service.RemoveCar(brand);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
