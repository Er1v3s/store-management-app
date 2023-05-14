using StoreManagementApp.pages;
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
            this.StateChanged += new EventHandler(Window_StateChanged);

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
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double taskbarHeight = SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Height - 7;

            if (this.WindowState == WindowState.Maximized)
            {
                windowBorder.Margin = new Thickness(7, 7, 7, 0);
                windowBorder.CornerRadius = new CornerRadius(0);
                this.MaxHeight = screenHeight - taskbarHeight;
                Leftbar.CornerRadius = new CornerRadius(0, 50, 0, 0);
            }
            else
            {
                windowBorder.Margin = new Thickness(0);
                windowBorder.CornerRadius = new CornerRadius(10);
                Leftbar.CornerRadius = new CornerRadius(0, 50, 0, 10);
            }
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Show_AddData_dialogBox(object sender, RoutedEventArgs e)
        {
            AddData AddDataWindow = new AddData();

            AddDataWindow.Show();
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
