using System;

namespace Minesweeper.Services
{
    public class PlayerDAO
    {
        //Connection credentials
        string connectionString = "";

        //Default constructor
        public PlayerDAO()
        {
        }

        public bool FindUserByNameAndPassword(UserModel user)
        {
            bool success = false;

            string sqlStatement = "SELECT * FROM dbo.users WHERE Username=@username and Password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar).Value = user.Password;
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
    }
}
