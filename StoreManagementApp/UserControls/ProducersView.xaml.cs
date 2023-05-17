using StoreManagementApp.pages;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;


namespace StoreManagementApp.UserControls
{
    public partial class ProducersView : UserControl, IObserver
    {
        public ObservableCollection<Producer> Producers { get; set; }
        public ProducersView()
        {
            InitializeComponent();
            Producers = new ObservableCollection<Producer>();

            RefreshItemsList();
        }

        public void RefreshItemsList()
        {
            Producers.Clear();

            using (SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();

                string sql = "SELECT * FROM Producer";

                SQLiteCommand command = new(sql, dbconnection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string www = reader.GetString(2);
                        int tin = reader.GetInt32(3);
                        int phoneNumber = reader.GetInt32(4);
                        string email = reader.GetString(5);

                        Producers.Add(new Producer { Id = id, Name = name, Www = www, Tin = tin, PhoneNumber = phoneNumber, Email = email });

                    }
                }

                dbconnection.Close();
            }

            foundPositions.Text = Producers.Count.ToString() + " odnalezionych pozycji";
            producersDataGrid.ItemsSource = Producers;
        }

        private void DeleteProducer(object sender, RoutedEventArgs e)
        {

            Producer selectedItem = (Producer)producersDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                using SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath);
                dbconnection.Open();

                string sql = "DELETE FROM Producer WHERE id=@id";
                SQLiteCommand command = new(sql, dbconnection);
                command.Parameters.AddWithValue("@id", selectedItem.Id);
                command.ExecuteNonQuery();

                dbconnection.Close();
            }

            RefreshItemsList();
        }

        private void ShowAddProducerDialogBox(object sender, System.Windows.RoutedEventArgs e)
        {
            AddProducer AddProducerWindow = new();
            AddProducerWindow.Attach(this);
            AddProducerWindow.Show();
        }
    }
}
