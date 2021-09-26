using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;

namespace html_parser
{
    class db_work
    {

        SqlConnection Connection;
        public db_work()
        {
           
            SqlConnectionStringBuilder Connect = new SqlConnectionStringBuilder
            {
                DataSource = "A1CAIDA",
                InitialCatalog = "a1caida",
                IntegratedSecurity = true

            };
            Connection = new SqlConnection(Connect.ConnectionString);
            //Connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        }
        public void insert(Dictionary<string, int> words)
        {
            SqlCommand command = Connection.CreateCommand();
            foreach(var word in words)
            {
                command.CommandText = $"SELECT COUNT(*) FROM [dbo].[word_statistics] WHERE word ='{word.Key}'";

                int count = 0;
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        count = reader.GetInt32(0);

                Connection.Close();

                if (count == 0)
                {
                    command.CommandText = $"INSERT INTO [dbo].[word_statistics] VALUES ('{word.Key}', {word.Value})";
                    
                    try
                    {
                        Connection.Open();
                        command.ExecuteNonQuery();

                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        Connection.Close();
                    }
                    
                }
                else
                {
                    command.CommandText = $"UPDATE [dbo].[word_statistics] SET count= count + {word.Value} WHERE word = '{word.Key}'";
                    try
                    {
                        Connection.Open();
                        command.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        Connection.Close();
                    }
                }
                    
            }
            
        }
    }
}
