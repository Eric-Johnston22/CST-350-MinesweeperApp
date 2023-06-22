using Microsoft.AspNetCore.Mvc;
using Minesweeper.Services;
using Minesweeper.Models;

namespace Minesweeper.Controllers
{
    public class LoginController : Controller
    {
        SecurityService securityService = new SecurityService();
        
        MongoDAO mongoDAO = new MongoDAO();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginSubmit(PlayerModel model)
        {

            if(securityService.IsValid(model))
            {
                PlayerModel player = securityService.getPlayer(model);
                HttpContext.Session.SetInt32("uid",player.Id);
                HttpContext.Session.SetString("username",player.Username);
                return RedirectToAction("Index", "Game");
            } else
            {
                ViewBag.error = true;
                return View("Index");
            }
        }
    }
}
