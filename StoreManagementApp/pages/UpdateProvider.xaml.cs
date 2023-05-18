using System;
using System.Windows;
using System.Windows.Input;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

namespace StoreManagementApp.pages
{
    public partial class UpdateProvider : Window, IWindowManipulationMethods
    {
        private readonly List<IObserver> _providerObservers = new();

        private static int Id { get; set; }

        public UpdateProvider(int id, string name, string www, int phone, string email)
        {
            InitializeComponent();
            this.StateChanged += new EventHandler(Window_StateChanged);
            this.KeyDown += HandleEnterKey;

            UpdateProvider.Id = id;
            update_name.Text = name;
            update_www.Text = www;
            update_phone.Text = phone.ToString();
            update_email.Text = email;
        }

        private void EditProviderInDB(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(update_name.Text) ||
                string.IsNullOrWhiteSpace(update_www.Text) ||
                string.IsNullOrWhiteSpace(update_phone.Text) ||
                string.IsNullOrWhiteSpace(update_email.Text))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath))
            {
                dbconnection.Open();

                string sql = "UPDATE Provider SET name=@name, www=@www, phone_number=@phone, email=@email WHERE id=@id";
                SQLiteCommand command = new(sql, dbconnection);
                command.Parameters.AddWithValue("@id", UpdateProvider.Id);
                command.Parameters.AddWithValue("@name", update_name.Text);
                command.Parameters.AddWithValue("@www", update_www.Text);
                command.Parameters.AddWithValue("@phone", update_phone.Text);
                command.Parameters.AddWithValue("@email", update_email.Text);
                command.ExecuteNonQuery();

                dbconnection.Close();
            }

            foreach (var observer in _providerObservers)
            {
                observer.RefreshItemsList();
            }

            Window.GetWindow(this).Close();
        }

        private void HandleEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EditProviderInDB(sender, e);
            }
        }

        public void Attach(IObserver observer)
        {
            _providerObservers.Add(observer);
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
