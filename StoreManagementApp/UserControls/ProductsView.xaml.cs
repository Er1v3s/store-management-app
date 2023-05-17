using StoreManagementApp.pages;
using StoreManagementApp.types;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace StoreManagementApp.UserControls
{
    public partial class ProductsView : UserControl, IObserver
    {
        public ObservableCollection<Product> Products { get; set; }
        public ProductsView()
        {
            InitializeComponent();

            Products = new ObservableCollection<Product>();
            RefreshItemsList();
        }

        public void RefreshItemsList()
        {
            Products.Clear();

            using (SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();

                string sql = "SELECT * FROM Product";

                SQLiteCommand command = new(sql, dbconnection);
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

                        Products.Add(new Product { Id = Id, Name = name, Category = category, Producent = producent, Price = price, Availability = availability });

                    }
                }

                dbconnection.Close();
            }

            foundPositions.Text = Products.Count.ToString() + " odnalezionych pozycji";
            productsDataGrid.ItemsSource = Products;
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
                using SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath);
                dbconnection.Open();

                string sql = "DELETE FROM Product WHERE id_product=@id";
                SQLiteCommand command = new(sql, dbconnection);
                command.Parameters.AddWithValue("@id", selectedProduct.Id);
                command.ExecuteNonQuery();

                dbconnection.Close();
            }

            RefreshItemsList();
        }

        private void Search(string userInput)
        {
            Products.Clear();

            using (SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();

                string sql = "SELECT * FROM Product WHERE name LIKE '%' || @userInput || '%' OR category LIKE '%' || @userInput || '%' OR producent LIKE '%' || @userInput || '%' OR price LIKE '%' || @userInput || '%' OR availability LIKE '%' || @userInput";

                SQLiteCommand command = new(sql, dbconnection);
                command.Parameters.AddWithValue("@userInput", userInput);
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

                        Products.Add(new Product { Id = Id, Name = name, Category = category, Producent = producent, Price = price, Availability = availability });

                    }
                }

                dbconnection.Close();
            }

            foundPositions.Text = Products.Count.ToString() + " odnalezionych pozycji";
            productsDataGrid.ItemsSource = Products;
        }

        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search(txtFilter.Text);
        }

        private void ShowAddDataDialogBox(object sender, RoutedEventArgs e)
        {
            AddData AddDataWindow = new();
            AddDataWindow.Attach(this);
            AddDataWindow.Show();
        }

        private void ShowUpdateDataDialogBox(int id, string name, string category, string producent, int price, int availability)
        {
            UpdateData updateDataWindow = new(id, name, category, producent, price, availability);
            updateDataWindow.Attach(this);
            updateDataWindow.Show();
        }

    }
}
