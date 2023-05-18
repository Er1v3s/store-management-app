using System;
using System.Windows;
using System.Windows.Input;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

namespace StoreManagementApp.pages
{
    public partial class UpdateData : Window, IWindowManipulationMethods
    {
        private readonly List<IObserver> _productObservers = new();

        private static int Id { get; set; }

        public UpdateData(int id, string name, string category, string producent, int price, int availability)
        {
            InitializeComponent();
            this.StateChanged += new EventHandler(Window_StateChanged);
            this.KeyDown += HandleEnterKey;

            UpdateData.Id = id;
            update_name.Text = name;
            update_category.Text = category;
            update_producent.Text = producent;
            update_price.Text = price.ToString();
            update_availability.Text = availability.ToString();
        }

        private void EditDataInDB(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(update_name.Text) ||
                string.IsNullOrWhiteSpace(update_category.Text) ||
                string.IsNullOrWhiteSpace(update_producent.Text) ||
                string.IsNullOrWhiteSpace(update_availability.Text) ||
                string.IsNullOrWhiteSpace(update_price.Text))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();

                string sql = "UPDATE Product SET name=@name, category=@category, producent=@producent, price=@price, availability=@availability WHERE id_product=@id";
                SQLiteCommand command = new(sql, dbconnection);
                command.Parameters.AddWithValue("@id", UpdateData.Id);
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
                observer.RefreshItemsList();
            }

            Window.GetWindow(this).Close();
        }

        private void HandleEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EditDataInDB(sender, e);
            }
        }

        public void Attach(IObserver observer)
        {
            _productObservers.Add(observer);
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
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
