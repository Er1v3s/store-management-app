using System.Data.SQLite;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using OxyPlot.Legends;

namespace StoreManagementApp.UserControls
{
    public partial class HomePageView : UserControl
    {
        public HomePageView()
        {
            InitializeComponent();

            products.Text = "Produkty\n"  + GetProductsCount().ToString();
            providers.Text = "Dostawcy\n" + GetProvidersCount().ToString();
            producers.Text = "Producenci\n" + GetPruducersCount().ToString();

            SeriesCollection productsInStore = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Wędki",
                    Values = new ChartValues<int> { GetItemsCount("Wędki") },
                    DataLabels = true,

                },
                new PieSeries
                {
                    Title = "Kołowrotki",
                    Values = new ChartValues<int> { GetItemsCount("Kołowrotki") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Żyłki",
                    Values = new ChartValues<int> { GetItemsCount("Żyłki") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Spławiki",
                    Values = new ChartValues<int> { GetItemsCount("Spławiki") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Ciężarki",
                    Values = new ChartValues<int> { GetItemsCount("Ciężarki") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Haczyki",
                    Values = new ChartValues<int> { GetItemsCount("Haczyki") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Podbieraki",
                    Values = new ChartValues<int> { GetItemsCount("Podbieraki") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Siatki",
                    Values = new ChartValues<int> { GetItemsCount("Siatki") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Przynęty",
                    Values = new ChartValues<int> { GetItemsCount("Przynęty") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Zanęty",
                    Values = new ChartValues<int> { GetItemsCount("Zanęty") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Inne",
                    Values = new ChartValues<int> { GetItemsCount("Inne") },
                    DataLabels = true
                }
            };

            pieChart.Series = productsInStore;
            pieChart.LegendLocation = LegendLocation.Right;

            SeriesCollection producersInStore = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Shimano",
                    Values = new ChartValues<int> { GetProducersCount("Shimano") },
                    DataLabels = true,

                },
                new PieSeries
                {
                    Title = "Konger",
                    Values = new ChartValues<int> { GetProducersCount("Konger") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Abu Garcia",
                    Values = new ChartValues<int> { GetProducersCount("Abu Garcia") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Rapala",
                    Values = new ChartValues<int> { GetProducersCount("Rapala") },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Savage Gear",
                    Values = new ChartValues<int> { GetProducersCount("Savage Gear") },
                    DataLabels = true
                }
            };

            pieChartProducers.Series = producersInStore;
            pieChartProducers.LegendLocation = LegendLocation.Right;
        }

        private int GetProductsCount()
        {
            int numberOfProductTableRecords = 0;

            using (SQLiteConnection dbconnection = new SQLiteConnection(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();
                string sql = "SELECT COUNT(*) FROM Product";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbconnection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            numberOfProductTableRecords = reader.GetInt32(0);
                        }
                    }
                }

                dbconnection.Close();
            }

            return numberOfProductTableRecords;
        }

        private int GetProvidersCount()
        {
            int numberOfProviderTableRecords = 0;

            using (SQLiteConnection dbconnection = new SQLiteConnection(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();
                string sql = "SELECT COUNT(*) FROM Provider";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbconnection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            numberOfProviderTableRecords = reader.GetInt32(0);
                        }
                    }
                }

                dbconnection.Close();
            }

            return numberOfProviderTableRecords;
        }

        private int GetPruducersCount()
        {
            int numberOfProducerTableRecords = 0;

            using (SQLiteConnection dbconnection = new SQLiteConnection(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();
                string sql = "SELECT COUNT(*) FROM Producer";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbconnection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            numberOfProducerTableRecords = reader.GetInt32(0);
                        }
                    }
                }

                dbconnection.Close();
            }

            return numberOfProducerTableRecords;
        }

        private int GetItemsCount(string item)
        {
            int numberOfItems = 0;

            using (SQLiteConnection dbconnection = new SQLiteConnection(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();
                string sql = "SELECT COUNT(*) FROM Product WHERE category = @item";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbconnection))
                {
                    command.Parameters.AddWithValue("@item", item);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            numberOfItems = reader.GetInt32(0);
                        }
                    }
                }

                dbconnection.Close();
            }

            return numberOfItems;
        }

        private int GetProducersCount(string item)
        {
            int numberOfItems = 0;

            using (SQLiteConnection dbconnection = new SQLiteConnection(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();
                string sql = "SELECT COUNT(*) FROM Product WHERE producent = @item";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbconnection))
                {
                    command.Parameters.AddWithValue("@item", item);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            numberOfItems = reader.GetInt32(0);
                        }
                    }
                }

                dbconnection.Close();
            }

            return numberOfItems;
        }
    }
}
