using StoreManagementApp.pages;
using StoreManagementApp.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace StoreManagementApp
{
    public partial class MainWindow : Window, IWindowManipulationMethods
    {
        
        public MainWindow(string employee)
        {
            InitializeComponent();
            CC.Content = new HomePageView();
            this.StateChanged += new EventHandler(Window_StateChanged);

            employee_name.Text = employee;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new();
            loginWindow.Show();

            Window.GetWindow(this).Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double taskbarHeight = SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Height - 7;

            if (this.WindowState == WindowState.Maximized)
            {
                windowBorder.Margin = new Thickness(7, 7, 7, 0);
                windowBorder.CornerRadius = new CornerRadius(0);
                this.MaxHeight = screenHeight - taskbarHeight;
                Leftbar.CornerRadius = new CornerRadius(0, 50, 0, 0);
            }
            else
            {
                windowBorder.Margin = new Thickness(0);
                windowBorder.CornerRadius = new CornerRadius(10);
                Leftbar.CornerRadius = new CornerRadius(0, 50, 0, 10);
            }
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void NavButtonProducts_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new ProductsView();
        }

        private void NavButtonHomePage_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new HomePageView();
        }

        private void NavButtonProviders_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new ProvidersView();
        }

        private void NavButtonProducers_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new ProducersView();
        }
    }
}
