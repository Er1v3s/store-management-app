using StoreManagementApp.pages;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.SQLite;

namespace StoreManagementApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.StateChanged += new EventHandler(Window_StateChanged);

            var converter = new BrushConverter();
            ObservableCollection<Product> products = new ObservableCollection<Product>();

            string databaseLocation = "Data Source=E:\\VisualStudio\\StoreManagementApp\\StoreManagementApp\\database\\StoreManagementApp.db";

            using (SQLiteConnection dbconnection = new SQLiteConnection(databaseLocation))
            {
                dbconnection.Open();

                string sql = "SELECT * FROM Product";

                SQLiteCommand command = new SQLiteCommand(sql, dbconnection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int Id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string category = reader.GetString(2);
                        string producent = reader.GetString(3);
                        int availability = reader.GetInt32(4);
                        int price = reader.GetInt32(5);

                        products.Add(new Product { Id = Id, Name = name, Category = category, Producent = producent, Availability = availability, Price = price});

                    }
                }

                dbconnection.Close();
            }

            foundPositions.Text = products.Count.ToString() + " odnalezionych pozycji";
            productsDataGrid.ItemsSource = products;
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

        private void Show_AddData_dialogBox(object sender, RoutedEventArgs e)
        {
            AddData AddDataWindow = new AddData();

            AddDataWindow.Show();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new();
            loginWindow.Show();

            Window.GetWindow(this).Close();
        }
    }

    public class Product
    {
        public int? Id { get; set;}
        public string? Name { get; set;}
        public string? Category { get; set;}
        public string? Producent { get; set;}
        public int? Availability { get; set;}
        public int? Price { get; set;}
    }
}
