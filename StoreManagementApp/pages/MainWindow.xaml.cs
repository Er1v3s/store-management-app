using StoreManagementApp.pages;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace StoreManagementApp
{
    public partial class MainWindow : Window, IProductObserver
    {
        readonly static string databaseLocation = "Data Source=E:\\VisualStudio\\StoreManagementApp\\StoreManagementApp\\database\\StoreManagementApp.db";

        public ObservableCollection<Product> products { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.StateChanged += new EventHandler(Window_StateChanged);
            var converter = new BrushConverter();

            products = new ObservableCollection<Product>();
            RefreshProductList();
        }

        private void ShowAddDataDialogBox(object sender, RoutedEventArgs e)
        {
            AddData AddDataWindow = new AddData();
            AddDataWindow.Attach(this);
            AddDataWindow.Show();
        }

        private void ShowUpdateDataDialogBox(int id, string name, string category, string producent, int price, int availability)
        {
            UpdateData updateDataWindow = new UpdateData(id, name, category, producent, price, availability);
            updateDataWindow.Attach(this);
            updateDataWindow.Show();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new();
            loginWindow.Show();

            Window.GetWindow(this).Close();
        }

        public void RefreshProductList()
        {
            products.Clear();

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
                        int price = reader.GetInt32(4);
                        int availability = reader.GetInt32(5);

                        products.Add(new Product { Id = Id, Name = name, Category = category, Producent = producent, Price = price, Availability = availability});

                    }
                }

                dbconnection.Close();
            }

            foundPositions.Text = products.Count.ToString() + " odnalezionych pozycji";
            productsDataGrid.ItemsSource = products;
        }

        private void EditData(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = (Product)productsDataGrid.SelectedItem;

            if (selectedProduct != null)
            {
                ShowUpdateDataDialogBox(selectedProduct.Id, selectedProduct.Name, selectedProduct.Category, selectedProduct.Producent, selectedProduct.Price, selectedProduct.Availability);
            }
        }

        private void DeleteData(object sender, RoutedEventArgs e)
        {

            Product selectedProduct = (Product)productsDataGrid.SelectedItem;

            if (selectedProduct != null)
            {
                using (SQLiteConnection dbconnection = new SQLiteConnection(databaseLocation))
                {
                    dbconnection.Open();

                    string sql = "DELETE FROM Product WHERE id_product=@id";
                    SQLiteCommand command = new SQLiteCommand(sql, dbconnection);
                    command.Parameters.AddWithValue("@id", selectedProduct.Id);
                    command.ExecuteNonQuery();

                    dbconnection.Close();
                }
            }

            RefreshProductList();
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
    }
}
