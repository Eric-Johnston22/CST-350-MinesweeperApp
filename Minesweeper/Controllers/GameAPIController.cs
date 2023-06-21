using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;
using Minesweeper.Services;

namespace Minesweeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameAPIController : ControllerBase
    {
        IConnect gameDAO;

        public GameAPIController(IConnect gameDAO)
        {
            this.gameDAO = gameDAO;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GameModel>> Index()
        {
            return gameDAO.FindAllGames();
        }

        [HttpGet("showSavedGames")]
        public ActionResult<IEnumerable<GameModel>> ShowAllGames()
        {
            return gameDAO.FindAllGames();
        }

        [HttpGet("showSavedGames/{GameNumber}")]
        public ActionResult<GameModel> ShowOneGame(int GameNumber)
        {
            GameModel gameModel = new GameModel();
            gameModel.GameNumber = GameNumber;
            return gameDAO.FindGameByNumber(gameModel);
        }

        [HttpGet("deleteOneGame/{GameNumber}")]
        public ActionResult<IEnumerable<GameModel>> DeleteOneGame(int GameNumber) {
            GameModel gameModel = new GameModel();
            gameModel.GameNumber = GameNumber;
            bool deleted = gameDAO.DeleteGameByNumber(gameModel);
            return gameDAO.FindAllGames();        
        }
    }
}
