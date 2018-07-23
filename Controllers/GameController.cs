using CowsAndBulls.Game;
using CowsAndBulls.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace CowsAndBulls.Controllers
{
    public class GameController : Controller
    {

        //public static GamePlay MyGame = new GamePlay();

        public static GamePlay MyGame;

        [HttpGet]
        public IActionResult Index()
        {
            // HTTP 1.1 GET http://localhost:8080/Game/Didi?myNumber=5&myNumber2=0
            return View("NewGame", new EnterNumberViewModel());
        }

        [ActionName("Index")]
        [HttpPost]
        public IActionResult IndexEnterNumber(EnterNumberViewModel model)
        {
            // HTTP 1.1 GET http://localhost:8080/Game/Didi?myNumber=5&myNumber2=0
            MyGame = new GamePlay(model.Number);
            return RedirectToAction("GameTurn");
        }

        [HttpGet]
        public IActionResult GameTurn()
        {
            // HTTP 1.1 POST http://localhost:8080/Game/Didi
            // myNumber=5&myNumber2=0
            if (MyGame.CheckForWinner()) return View("EndGame", MyGame.WhoWon());
            return View("GameTurn", MyGame.GetScene());
        }

        public IActionResult EnterNumber(EnterNumberViewModel model)
        {
            if (MyGame.CheckForWinner()) return View("EndGame", MyGame.WhoWon());
            MyGame.Tick(model);
            return RedirectToAction("GameTurn");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}