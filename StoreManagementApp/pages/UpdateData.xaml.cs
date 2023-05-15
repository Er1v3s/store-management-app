using System;
using System.Windows;
using System.Windows.Input;
using System.Data.SQLite;
using System.Collections.Generic;

namespace StoreManagementApp.pages
{
    /// <summary>
    /// Interaction logic for UpdateData.xaml
    /// </summary>
    public partial class UpdateData : Window
    {
        private List<IProductObserver> _productObservers = new List<IProductObserver>();

        string databaseLocation = "Data Source=E:\\VisualStudio\\StoreManagementApp\\StoreManagementApp\\database\\StoreManagementApp.db";

        private static int id { get; set; }

        public UpdateData(int id, string name, string category, string producent, int price, int availability)
        {
            InitializeComponent();
            this.StateChanged += new EventHandler(Window_StateChanged);

            UpdateData.id = id;
            update_name.Text = name;
            update_category.Text = category;
            update_producent.Text = producent;
            update_price.Text = price.ToString();
            update_availability.Text = availability.ToString();
        }

        private void EditDataInDB(object sender, EventArgs e)
        {
            using (SQLiteConnection dbconnection = new SQLiteConnection(databaseLocation))
            {
                dbconnection.Open();

                string sql = "UPDATE Product SET name=@name, category=@category, producent=@producent, price=@price, availability=@availability WHERE id_product=@id";
                SQLiteCommand command = new SQLiteCommand(sql, dbconnection);
                command.Parameters.AddWithValue("@id", UpdateData.id);
                command.Parameters.AddWithValue("@name", update_name.Text);
                command.Parameters.AddWithValue("@category", update_category.Text);
                command.Parameters.AddWithValue("@producent", update_producent.Text);
                command.Parameters.AddWithValue("@availability", update_availability.Text);
                command.Parameters.AddWithValue("@price", update_price.Text);
                command.ExecuteNonQuery();

                dbconnection.Close();
            }

            foreach (var observer in _productObservers)
            {
                observer.RefreshProductList();
            }

            Window.GetWindow(this).Close();
        }

        public void Attach(IProductObserver observer)
        {
            _productObservers.Add(observer);
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
                topBar.CornerRadius = new CornerRadius(0);
            }
            else
            {
                windowBorder.Margin = new Thickness(0);
                windowBorder.CornerRadius = new CornerRadius(10);
                topBar.CornerRadius = new CornerRadius(5, 5, 0, 0);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AreAllValidNumericChars(e.Text);
        }

        private static bool AreAllValidNumericChars(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
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
