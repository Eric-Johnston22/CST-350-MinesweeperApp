using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;
using Minesweeper.Services;
using NuGet.Protocol.Plugins;
using System.Text.Json;

namespace Minesweeper.Controllers
{
    public class GameController : Controller
    {
        GameDAO gameDAO = new GameDAO();
        static BoardService boardService = new BoardService(10);

        public static BoardService temp = new BoardService(10);

        public IActionResult Index()
        {
            ViewBag.Size = boardService.Size;
            return View("Index");
        }
        public IActionResult LeftButtonClick(string buttonNumber)
        {

            string[] position = buttonNumber.Split(',');
            int x = int.Parse(position[0]);
            int y = int.Parse(position[1]);
            

            if (boardService.Grid[x, y].Live == true)
            {
                boardService.Exploded = true;
            }
            else
            {
                boardService.floodFill(x, y);

                //Check if it was a winning move
                if (boardService.RemainingCells == boardService.NumberOfBombs)
                {
                    boardService.GameWon = true;
                }
            }

            return PartialView("LeftButtonClick",boardService);
        
        }
        public IActionResult RightButtonClick(string buttonNumber)
        {
            string[] position = buttonNumber.Split(',');
            int x = int.Parse(position[0]);
            int y = int.Parse(position[1]);
            bool currentlyFlagged = boardService.Grid[x, y].Flagged;
            if(currentlyFlagged == true)
            {
                boardService.Grid[x,y].Flagged = false;
            } else
            {
                boardService.Grid[x, y].Flagged = true;
            }
            CellModel toFlag = boardService.Grid[x, y];
            return PartialView("RightButtonClick",toFlag);
        }
        public IActionResult NewGame()
        {
            boardService = new BoardService(10);
            ViewBag.Size = boardService.Size;
            return View("Index");
        }
        public IActionResult SaveGame()
        {
            string gameData = "";
            for(int i=0;i<boardService.Size;i++)
            {
                for(int j=0;j<boardService.Size;j++)
                {
                    gameData += boardService.Grid[i, j].ToString();
                    gameData += "&";
                }
            }
            gameData += boardService.Exploded;
            gameData += "&"+boardService.GameWon;
            gameData += "&" + boardService.NumberOfBombs;
            gameData += "&" + boardService.RemainingCells;
            gameData += "&" + boardService.Size;
            int uid = HttpContext.Session.GetInt32("uid").GetValueOrDefault();
            GameModel game = new GameModel(uid,gameData);
            bool done = gameDAO.AddGameToDatabase(game);
            return Content(""+done);
        }

        public IActionResult ShowSavedGames()
        {
            int uid = HttpContext.Session.GetInt32("uid").GetValueOrDefault();
            PlayerModel player = new PlayerModel();
            player.Id = uid;
            List<GameModel> games = gameDAO.FindGamesById(player);
            return PartialView("ShowSavedGames",games);
        }

        public IActionResult LoadGame(string gameNumber)
        {
            int num = Int32.Parse(gameNumber);
            GameModel loaded = new GameModel();
            loaded.GameNumber = num;
            gameDAO.GetGameByNumber(loaded);
            boardService = temp;
            ViewBag.Size = boardService.Size;
            return PartialView("LeftButtonClick",boardService);
        }

        public IActionResult DeleteGame(string gameNumber)
        {
            int num = Int32.Parse(gameNumber);
            GameModel del = new GameModel();
            del.GameNumber = num;
            bool success = gameDAO.DeleteGameByNumber(del);
            return Content(""+success);
        }

        public IActionResult Welcome()
        {
            return Content("Welcome, "+HttpContext.Session.GetString("username")+", your user id is: " + HttpContext.Session.GetInt32("uid"));
        }
    }
}
