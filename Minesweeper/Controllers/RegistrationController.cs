using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;
using Minesweeper.Services;

namespace Minesweeper.Controllers
{
    public class RegistrationController : Controller
    {
        SecurityService securityService = new SecurityService();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register(PlayerModel player)
        {

            if(securityService.alreadyExists(player))
            {
                ViewBag.playerExists = true;
                return View("Index");
            } else
            {
                bool registrationSuccess = securityService.addPlayer(player);
                ViewBag.registered = registrationSuccess;
                if(registrationSuccess)
                {
                    return View("~/Views/Login/Index.cshtml");
                } else
                {
                    return View("Index");
                }

            }
        }
    }
}
