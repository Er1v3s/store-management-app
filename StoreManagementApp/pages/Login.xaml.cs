using System.Windows;
using System.Windows.Input;


namespace StoreManagementApp.pages
{
    public partial class Login : Window, IWindowManipulationMethods
    {
        public Login()
        {
            InitializeComponent();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Move_To_MainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();

            Window.GetWindow(this).Close();
        }
    }
}
