using Minesweeper.Models;

namespace Minesweeper.Services
{
    public interface IConnect
    {
        public List<GameModel> FindGamesById(PlayerModel player);

        public List<GameModel> FindAllGames();

        public GameModel FindGameByNumber(GameModel game);

        public BoardService GetGameByNumber(GameModel game);

        public bool DeleteGameByNumber(GameModel game);

        public bool AddGameToDatabase(GameModel game);

    }
}
