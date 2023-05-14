using System;
using System.Collections.ObjectModel;
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
        public AddData()
        {
            InitializeComponent();

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


        private readonly WindowHelper _windowHelper = new WindowHelper();

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            _windowHelper.DragWindow(sender, e);
        }

        private void CloseWindowButton(object sender, RoutedEventArgs e)
        {
            _windowHelper.CloseWindow(sender, e);
        }

        private void MinimalizeWindowButton(object sender, RoutedEventArgs e)
        {
            _windowHelper.MinimalizeWindow(sender, e);
        }

        private void MaximalizeWindowButton(object sender, RoutedEventArgs e)
        {
            _windowHelper.MaximalizeWindow(sender, e);
        }
    }
}
