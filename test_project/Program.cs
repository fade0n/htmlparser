using System;
using System.Data.SqlClient;

namespace test_project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SQL();
        }

        private static void SQL()
        {
            SqlConnectionStringBuilder Connect = new SqlConnectionStringBuilder
            {
                DataSource = "A1CAIDA",
                InitialCatalog = "master",
                IntegratedSecurity = true
            };
            var str = Connect.ToString();
            using (SqlConnection connection = new SqlConnection(str))
            {
                _ = connection.ConnectionTimeout;
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM word_statistics", connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var a = reader["Column"];//инициализация значения переменной полем из таблицы БД
                    }
                }
            }
        }
    }
}
