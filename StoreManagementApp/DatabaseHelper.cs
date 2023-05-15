using System.IO;

namespace StoreManagementApp
{
    public static class DatabaseHelper
    {
        public static string DatabasePath { get; private set; }

        static DatabaseHelper()
        {
            string databaseLocation = @"..\..\..\database\StoreManagementApp.db";
            string projectPath = Directory.GetCurrentDirectory();
            string absolutePath = Path.Combine(projectPath, databaseLocation);

            DatabasePath = "Data Source="+absolutePath;
        }
    }
}
