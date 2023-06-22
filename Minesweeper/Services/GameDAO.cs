using Minesweeper.Controllers;
using Minesweeper.Models;
using System.Data.SqlClient;

namespace Minesweeper.Services
{
    public class GameDAO : IConnect
    {
        //Connection credentials
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Minesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public GameDAO()
        {
        }

        public List<GameModel> FindGamesById(PlayerModel player)
        {
            string sqlStatement = "SELECT * FROM dbo.game WHERE Id=@Id";
            List<GameModel> gameList = new List<GameModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value =player.Id;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        GameModel gotten = new GameModel(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2),reader.GetString(3),reader.GetString(4));
                        gameList.Add(gotten);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return gameList;
        }

        public List<GameModel> FindAllGames()
        {
            string sqlStatement = "SELECT * FROM dbo.game";
            List<GameModel> gameList = new List<GameModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        GameModel gotten = new GameModel(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                        gameList.Add(gotten);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return gameList;
        }

        public GameModel FindGameByNumber(GameModel game)
        {
            string sqlStatement = "SELECT * FROM dbo.game WHERE GameNumber=@GameNumber";
            GameModel gotten  = new GameModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@GameNumber", game.GameNumber);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        gotten = new GameModel(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return gotten;
        }

        public BoardService GetGameByNumber(GameModel game)
        {
            string sqlStatement = "SELECT * FROM dbo.game WHERE GameNumber=@GameNumber";
            BoardService temp = new BoardService(10);
            CellModel[,] cellModel = new CellModel[10,10];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@GameNumber", System.Data.SqlDbType.Int).Value = game.GameNumber;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string gameData = reader.GetString(4);
                        string[] cells = gameData.Split('&');
                        temp = new BoardService(bool.Parse(cells[cells.Length - 5]), bool.Parse(cells[cells.Length - 4]),Int32.Parse(cells[cells.Length - 3]),Int32.Parse(cells[cells.Length - 2]), Int32.Parse(cells[cells.Length - 1]));

                        for (int i=0;i<cells.Length-5;i++)
                        {
                            string[] data = cells[i].Split(",");
                            CellModel singleCell = new CellModel();
                            for(int j=0;j<data.Length;j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        singleCell.Visited = bool.Parse(data[j]);
                                        break;

                                    case 1:
                                        singleCell.Live = bool.Parse(data[j]);
                                        break;

                                    case 2:
                                        singleCell.LiveNeighbors = Int32.Parse(data[j]);
                                        break;

                                    case 3:
                                        singleCell.Flagged = bool.Parse(data[j]);
                                        break;

                                    case 4:
                                        singleCell.Row = Int32.Parse(data[j]);
                                        break;

                                    case 5:
                                        singleCell.Column = Int32.Parse(data[j]);
                                        break;
                                }
                            }
                            cellModel[singleCell.Row,singleCell.Column] = singleCell;
                        }
                        
                        break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            temp.Grid = cellModel;
            return temp;
        }
        public bool DeleteGameByNumber(GameModel game)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlStatement = "DELETE FROM dbo.game WHERE GameNumber=@GameNumber";
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@GameNumber", game.GameNumber);
                int affected = command.ExecuteNonQuery();
                if (affected > 0)
                {
                    success = true;
                }
            }
            return success;
        }

        public bool AddGameToDatabase(GameModel game)
        {
            bool success = false;

            string sqlStatement = "INSERT INTO dbo.game(Id, GameTime, GameDate, GameData) VALUES (@Id,@GameTime,@GameDate,@GameData)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = game.Id;
                command.Parameters.Add("@GameTime", System.Data.SqlDbType.VarChar, 15).Value = game.Time;
                command.Parameters.Add("@GameDate", System.Data.SqlDbType.VarChar, 15).Value = game.Date;
                command.Parameters.Add("@GameData", System.Data.SqlDbType.VarChar,3000).Value = game.GameData;

                try
                {
                    connection.Open();
                    int affected = command.ExecuteNonQuery();

                    if (affected > 0)
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
