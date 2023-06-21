using Minesweeper.Controllers;
using Minesweeper.Models;
using System.Data.SqlClient;

namespace Minesweeper.Services
{
    public class MongoDAO : IConnect
    {
        public bool AddGameToDatabase(GameModel game)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGameByNumber(GameModel game)
        {
            throw new NotImplementedException();
        }

        public List<GameModel> FindAllGames()
        {
            throw new NotImplementedException();
        }

        public GameModel FindGameByNumber(GameModel game)
        {
            throw new NotImplementedException();
        }

        public List<GameModel> FindGamesById(PlayerModel player)
        {
            throw new NotImplementedException();
        }

        public CellModel[,] GetGameByNumber(GameModel game)
        {
            throw new NotImplementedException();
        }
    }
}
