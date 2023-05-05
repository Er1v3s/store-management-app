using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StoreManagementApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StateChanged += Window_StateChanged;

            var converter = new BrushConverter();
            ObservableCollection<Product> products = new ObservableCollection<Product>();

            // Create DataGrid Items Info
            products.Add(new Product { id = "1", Name = "Wędka shimano", Category ="Wędki", Amount = "513", Provider = "John Doe" });
            products.Add(new Product { id = "2", Name = "Haczyk konger", Category = "Haczyki", Amount = "764", Provider = "John Doe" });
            products.Add(new Product { id = "3", Name = "Ciężarki bakalimo", Category = "ciężarki", Amount = "123", Provider = "John Doe" });
            products.Add(new Product { id = "4", Name = "Podbierak wudusu", Category = "podbieraki", Amount = "456", Provider = "John Doe" });
            products.Add(new Product { id = "5", Name = "Siatka ligthe", Category = "siatki", Amount = "654", Provider = "John Doe" });
            products.Add(new Product { id = "6", Name = "Wędka geek bar", Category = "Wędki", Amount = "323", Provider = "John Doe" });
            products.Add(new Product { id = "7", Name = "Wędka mozos", Category = "Wędki", Amount = "163", Provider = "John Doe" });
            products.Add(new Product { id = "8", Name = "Kołowrotek belinmo", Category = "kołowrotki", Amount = "133", Provider = "John Doe" });
            products.Add(new Product { id = "9", Name = "Kołowrotek watcher", Category = "kołowrotki", Amount = "213", Provider = "John Doe" });
            products.Add(new Product { id = "10", Name = "Spławik przelotowy hakimito", Category = "Spławiki", Amount = "5113", Provider = "John Doe" });

            products.Add(new Product { id = "1", Name = "Wędka shimano", Category = "Wędki", Amount = "513", Provider = "John Doe" });
            products.Add(new Product { id = "2", Name = "Haczyk konger", Category = "Haczyki", Amount = "764", Provider = "John Doe" });
            products.Add(new Product { id = "3", Name = "Ciężarki bakalimo", Category = "ciężarki", Amount = "123", Provider = "John Doe" });
            products.Add(new Product { id = "4", Name = "Podbierak wudusu", Category = "podbieraki", Amount = "456", Provider = "John Doe" });
            products.Add(new Product { id = "5", Name = "Siatka ligthe", Category = "siatki", Amount = "654", Provider = "John Doe" });
            products.Add(new Product { id = "6", Name = "Wędka geek bar", Category = "Wędki", Amount = "323", Provider = "John Doe" });
            products.Add(new Product { id = "7", Name = "Wędka mozos", Category = "Wędki", Amount = "163", Provider = "John Doe" });
            products.Add(new Product { id = "8", Name = "Kołowrotek belinmo", Category = "kołowrotki", Amount = "133", Provider = "John Doe" });
            products.Add(new Product { id = "9", Name = "Kołowrotek watcher", Category = "kołowrotki", Amount = "213", Provider = "John Doe" });
            products.Add(new Product { id = "10", Name = "Spławik przelotowy hakimito", Category = "Spławiki", Amount = "5113", Provider = "John Doe" });

            products.Add(new Product { id = "1", Name = "Wędka shimano", Category = "Wędki", Amount = "513", Provider = "John Doe" });
            products.Add(new Product { id = "2", Name = "Haczyk konger", Category = "Haczyki", Amount = "764", Provider = "John Doe" });
            products.Add(new Product { id = "3", Name = "Ciężarki bakalimo", Category = "ciężarki", Amount = "123", Provider = "John Doe" });
            products.Add(new Product { id = "4", Name = "Podbierak wudusu", Category = "podbieraki", Amount = "456", Provider = "John Doe" });
            products.Add(new Product { id = "5", Name = "Siatka ligthe", Category = "siatki", Amount = "654", Provider = "John Doe" });
            products.Add(new Product { id = "6", Name = "Wędka geek bar", Category = "Wędki", Amount = "323", Provider = "John Doe" });
            products.Add(new Product { id = "7", Name = "Wędka mozos", Category = "Wędki", Amount = "163", Provider = "John Doe" });
            products.Add(new Product { id = "8", Name = "Kołowrotek belinmo", Category = "kołowrotki", Amount = "133", Provider = "John Doe" });
            products.Add(new Product { id = "9", Name = "Kołowrotek watcher", Category = "kołowrotki", Amount = "213", Provider = "John Doe" });
            products.Add(new Product { id = "10", Name = "Spławik przelotowy hakimito", Category = "Spławiki", Amount = "5113", Provider = "John Doe" });

            products.Add(new Product { id = "1", Name = "Wędka shimano", Category = "Wędki", Amount = "513", Provider = "John Doe" });
            products.Add(new Product { id = "2", Name = "Haczyk konger", Category = "Haczyki", Amount = "764", Provider = "John Doe" });
            products.Add(new Product { id = "3", Name = "Ciężarki bakalimo", Category = "ciężarki", Amount = "123", Provider = "John Doe" });
            products.Add(new Product { id = "4", Name = "Podbierak wudusu", Category = "podbieraki", Amount = "456", Provider = "John Doe" });
            products.Add(new Product { id = "5", Name = "Siatka ligthe", Category = "siatki", Amount = "654", Provider = "John Doe" });
            products.Add(new Product { id = "6", Name = "Wędka geek bar", Category = "Wędki", Amount = "323", Provider = "John Doe" });
            products.Add(new Product { id = "7", Name = "Wędka mozos", Category = "Wędki", Amount = "163", Provider = "John Doe" });
            products.Add(new Product { id = "8", Name = "Kołowrotek belinmo", Category = "kołowrotki", Amount = "133", Provider = "John Doe" });
            products.Add(new Product { id = "9", Name = "Kołowrotek watcher", Category = "kołowrotki", Amount = "213", Provider = "John Doe" });
            products.Add(new Product { id = "10", Name = "Spławik przelotowy hakimito", Category = "Spławiki", Amount = "5113", Provider = "John Doe" });


            productsDataGrid.ItemsSource = products;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                windowBorder.CornerRadius = new CornerRadius(0);
                windowBorder.BorderThickness = new Thickness(7);
            }
            else
            {
                windowBorder.CornerRadius = new CornerRadius(10);
                windowBorder.BorderThickness = new Thickness(0);
            }
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if(IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }

        private void CloseWindowButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimalizeWindowButton(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximalizeWindowButton(object sender, RoutedEventArgs e)
        {
            if (IsMaximized)
            {
                this.WindowState = WindowState.Normal;
                this.Width = 1080;
                this.Height = 720;

                IsMaximized = false;
            }
            else
            {
                this.WindowState = WindowState.Maximized;

                IsMaximized = true;
            }
        }

    }

    public class Product
    {
        public string? id { get; set;}
        public string? Name { get; set;}
        public string? Category { get; set;}
        public string? Amount { get; set;}
        public string? Provider { get; set;}
    }
}
