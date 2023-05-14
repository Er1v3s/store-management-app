using System;
using System.Windows;
using System.Windows.Controls;


namespace StoreManagementApp.UserControls
{
    /// <summary>
    /// Interaction logic for WindowManipulation.xaml
    /// </summary>
    public partial class WindowManipulation : UserControl
    {
        public WindowManipulation()
        {
            InitializeComponent();
        }

        private void CloseWindowButton(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if(window != Application.Current.MainWindow)
            {
                window.Close();
            } 
            else
            {
                Application.Current.Shutdown();
            }


        }

        private void MinimalizeWindowButton(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }

        private void MaximalizeWindowButton(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }
    }
}
