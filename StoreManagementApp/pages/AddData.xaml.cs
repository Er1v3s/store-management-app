﻿using StoreManagementApp.pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StoreManagementApp.pages
{
    /// <summary>
    /// Interaction logic for AddData.xaml
    /// </summary>
    public partial class AddData : Window
    {
        private List<IProductObserver> _productObservers = new List<IProductObserver>();

        string databaseLocation = "Data Source=E:\\VisualStudio\\StoreManagementApp\\StoreManagementApp\\database\\StoreManagementApp.db";
        public AddData()
        {
            InitializeComponent();
            this.StateChanged += new EventHandler(Window_StateChanged);
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

        private void AddDataToDB(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection dbconnection = new SQLiteConnection(databaseLocation))
            {
                dbconnection.Open();

                string sql = "INSERT INTO Product VALUES(@id_product, @name, @category, @producent, @price, @availability)";
                SQLiteCommand command = new SQLiteCommand(sql, dbconnection);
                command.Parameters.AddWithValue("@id_product", null);
                command.Parameters.AddWithValue("@name", name.Text);
                command.Parameters.AddWithValue("@category", category.Text);
                command.Parameters.AddWithValue("@producent", producent.Text);
                command.Parameters.AddWithValue("@price", price.Text);
                command.Parameters.AddWithValue("@availability", availability.Text);
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
    }
}
