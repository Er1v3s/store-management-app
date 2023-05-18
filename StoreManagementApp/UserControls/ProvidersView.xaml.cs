using StoreManagementApp.pages;
using StoreManagementApp.types;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoreManagementApp.UserControls
{
    public partial class ProvidersView : UserControl, IObserver
    {
        public ObservableCollection<Provider> Providers { get; set; }
        public ProvidersView()
        {
            InitializeComponent();
            Providers = new ObservableCollection<Provider>();

            RefreshItemsList();
        }

        public void RefreshItemsList()
        {
            Providers.Clear();

            using (SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();

                string sql = "SELECT * FROM Provider";

                SQLiteCommand command = new(sql, dbconnection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string www = reader.GetString(2);
                        int phoneNumber = reader.GetInt32(3);
                        string email = reader.GetString(4);

                        Providers.Add(new Provider { Id = id, Name = name, Www = www, PhoneNumber = phoneNumber, Email = email });

                    }
                }

                dbconnection.Close();
            }

            foundPositions.Text = Providers.Count.ToString() + " odnalezionych pozycji";
            providersDataGrid.ItemsSource = Providers;
        }

        private void EditProvider(object sender, RoutedEventArgs e)
        {
            Provider selectedItem = (Provider)providersDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                ShowUpdateProviderDialogBox(selectedItem.Id, selectedItem.Name, selectedItem.Www, selectedItem.PhoneNumber, selectedItem.Email);
            }
        }

        private void DeleteProvider(object sender, RoutedEventArgs e)
        {

            Provider selectedItem = (Provider)providersDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                using SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath);
                dbconnection.Open();

                string sql = "DELETE FROM Provider WHERE id=@id";
                SQLiteCommand command = new(sql, dbconnection);
                command.Parameters.AddWithValue("@id", selectedItem.Id);
                command.ExecuteNonQuery();

                dbconnection.Close();
            }

            RefreshItemsList();
        }

        private void Search(string userInput)
        {
            Providers.Clear();

            using (SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();

                string sql = "SELECT * FROM Provider WHERE name LIKE '%' || @userInput || '%' OR www LIKE '%' || @userInput || '%' OR phone_number LIKE '%' || @userInput || '%' OR email LIKE '%' || @userInput || '%'";

                SQLiteCommand command = new(sql, dbconnection);
                command.Parameters.AddWithValue("@userInput", userInput);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string www = reader.GetString(2);
                        int phoneNumber = reader.GetInt32(3);
                        string email = reader.GetString(4);

                        Providers.Add(new Provider { Id = id, Name = name, Www = www, PhoneNumber = phoneNumber, Email = email });

                    }
                }

                dbconnection.Close();
            }

            foundPositions.Text = Providers.Count.ToString() + " odnalezionych pozycji";
            providersDataGrid.ItemsSource = Providers;
        }

        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search(txtFilter.Text);
        }

        private void ShowAddProviderDialogBox(object sender, RoutedEventArgs e)
        {
            AddProvider AddProviderWindow = new();
            AddProviderWindow.Attach(this);
            AddProviderWindow.Show();
        }

        private void ShowUpdateProviderDialogBox(int id, string name, string www, int phone, string email)
        {
            UpdateProvider updateProviderWindow = new(id, name, www, phone, email);
            updateProviderWindow.Attach(this);
            updateProviderWindow.Show();
        }
    }
}
