﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;

namespace StoreManagementApp.pages
{
    public partial class AddProvider : Window, IWindowManipulationMethods
    {
        private readonly List<IObserver> _providerObservers = new();
        public AddProvider()
        {
            InitializeComponent();
            this.StateChanged += new EventHandler(Window_StateChanged);
            this.KeyDown += HandleEnterKey;
        }

        private void AddProviderToDB(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath))
            {
                if (string.IsNullOrWhiteSpace(name.Text) ||
                string.IsNullOrWhiteSpace(www.Text) ||
                string.IsNullOrWhiteSpace(phone.Text) ||
                string.IsNullOrWhiteSpace(email.Text))
                {
                    MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dbconnection.Open();

                string sql = "INSERT INTO Provider VALUES(@id, @name, @www, @phone_number, @email)";
                SQLiteCommand command = new(sql, dbconnection);
                command.Parameters.AddWithValue("@id", null);
                command.Parameters.AddWithValue("@name", name.Text);
                command.Parameters.AddWithValue("@www", www.Text);
                command.Parameters.AddWithValue("@phone_number", phone.Text);
                command.Parameters.AddWithValue("@email", email.Text);
                command.ExecuteNonQuery();

                dbconnection.Close();
            }

            foreach (var observer in _providerObservers)
            {
                observer.RefreshItemsList();
            }

            Window.GetWindow(this).Close();
        }

        public void Attach(IObserver observer)
        {
            _providerObservers.Add(observer);
        }

        private void HandleEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddProviderToDB(sender, e);
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
