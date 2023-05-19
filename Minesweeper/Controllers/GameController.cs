using Microsoft.AspNetCore.Mvc;
using Minesweeper.Services;
using NuGet.Protocol.Plugins;

namespace Minesweeper.Controllers
{
    public class GameController : Controller
    {
        static BoardService boardService = new BoardService(10);
        public IActionResult Index()
        {
            ViewBag.board = boardService;
            return View("Index");
        }
        public IActionResult HandleButtonClick(string buttonNumber)
        {
            string[] position = buttonNumber.Split(',');
            int x = int.Parse(position[0]);
            int y = int.Parse(position[1]);

            if (boardService.Grid[x, y].Live == true)
            {
                ViewBag.exploded = true;
            }
            else
            {
                boardService.floodFill(x, y);

                //Check if it was a winning move
                if (boardService.RemainingCells == boardService.NumberOfBombs)
                {
                    //Mark all cells with bombs as flagged
                    //makeButtonsFlagged();

                    //Call winning message
                    //youWin();
                }
            }

            ViewBag.board = boardService;

            return View("Index");
        }

        public IActionResult NewGame()
        {
            boardService = new BoardService(15);
            ViewBag.board = boardService;
            return View("Index");
        }

    }
}
