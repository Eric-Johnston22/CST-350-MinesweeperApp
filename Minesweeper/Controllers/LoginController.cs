using Microsoft.AspNetCore.Mvc;
using Minesweeper.Services;
using Minesweeper.Models;

namespace Minesweeper.Controllers
{
    public class LoginController : Controller
    {
        SecurityService securityService = new SecurityService();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginSubmit(PlayerModel model)
        {

            if(securityService.IsValid(model))
            {
                HttpContext.Session.SetInt32("uid",model.Id);
                HttpContext.Session.SetString("username",model.Username);
                return RedirectToAction("Index", "Game");
            } else
            {
                ViewBag.error = true;
                return View("Index");
            }
        }
    }
}
