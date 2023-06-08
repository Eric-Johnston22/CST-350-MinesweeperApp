using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;
using Minesweeper.Services;
using NuGet.Protocol.Plugins;

namespace Minesweeper.Controllers
{
    public class GameController : Controller
    {
        static BoardService boardService = new BoardService(10);
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

        public IActionResult Welcome()
        {
            return Content("Welcome, "+HttpContext.Session.GetString("username")+", your user id is: " + HttpContext.Session.GetInt32("uid"));
        }
    }
}
