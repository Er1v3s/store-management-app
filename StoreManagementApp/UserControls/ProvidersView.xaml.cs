using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows.Controls;


namespace StoreManagementApp.UserControls
{
    public partial class ProvidersView : UserControl
    {
        public ObservableCollection<Provider> Providers { get; set; }
        public ProvidersView()
        {
            InitializeComponent();
            Providers = new ObservableCollection<Provider>();

            RefreshProvidersList();
        }

        public void RefreshProvidersList()
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
            productsDataGrid.ItemsSource = Providers;
        }
    }
}
