using System;
using System.Data.SqlClient;
using Minesweeper.Models;
namespace Minesweeper.Services
{
    public class PlayerDAO
    {
        //Connection credentials
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Minesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Default constructor
        public PlayerDAO()
        {
        }

        public bool FindUserByName(PlayerModel player)
        {
            bool success = false;

            string sqlStatement = "SELECT * FROM dbo.player WHERE username=@username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 255).Value = player.Username;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return success;
        }

        public bool FindUserByNameAndPassword(PlayerModel player)
        {
            bool success = false;

            string sqlStatement = "SELECT * FROM dbo.player WHERE username=@username and password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 255).Value = player.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar).Value = player.Password;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return success;
        }
        public bool AddPlayerToDatabase(PlayerModel player)
        {
            bool success = false;

            string sqlStatement = "INSERT INTO dbo.player(first_name, last_name, sex, age, state, email, username, password) VALUES (@firstname,@lastname,@sex,@age,@state,@email,@username,@password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@FIRSTNAME", System.Data.SqlDbType.VarChar, 80).Value = player.FirstName;
                command.Parameters.Add("@LASTNAME", System.Data.SqlDbType.VarChar, 80).Value = player.LastName;
                command.Parameters.Add("@SEX", System.Data.SqlDbType.VarChar,80).Value = player.Sex;
                command.Parameters.Add("@AGE", System.Data.SqlDbType.Int).Value = player.Age;
                command.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar, 255).Value = player.State;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.VarChar, 255).Value = player.Email;
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 255).Value = player.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 255).Value = player.Password;

                try
                {
                    connection.Open();
                    int affected = command.ExecuteNonQuery();

                    if (affected>0)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return success;
        }
    }
}
