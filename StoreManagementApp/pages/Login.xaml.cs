using System;
using System.Data.Common;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;


namespace StoreManagementApp.pages
{
    public partial class Login : Window, IWindowManipulationMethods
    {
        public Login()
        {
            InitializeComponent();
            this.KeyDown += HandleEnterKey;
        }

        public string? employee;

        private void MoveToMainWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                if (employee_login.Text != "" && employee_password.Password != "")
                {
                    using SQLiteConnection dbconnection = new(DatabaseHelper.DatabasePath);
                    dbconnection.Open();

                    string sql = "SELECT * FROM Employee WHERE login=@login and password=@password";

                    SQLiteCommand command = new(sql, dbconnection);
                    command.Parameters.AddWithValue("@login", employee_login.Text);
                    command.Parameters.AddWithValue("@password", employee_password.Password);

                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employee = reader.GetString(3);
                        }

                        MainWindow mainWindow = new(employee);
                        mainWindow.Show();

                        Window.GetWindow(this).Close();
                    }
                    else
                    {
                        throw new Exception("Login lub hasło nieprawidłowe");
                    }

                    dbconnection.Close();
                }
                else
                {
                    throw new Exception("Wprowadź login i hasło");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                employee_password.Password = "";
                employee_login.Text = "";
            }
        }

        private void HandleEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(employee_login.Text != "" && employee_password.Password != "")
                {
                    MoveToMainWindow(sender, e);
                    Keyboard.Focus(employee_login);
                }
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
