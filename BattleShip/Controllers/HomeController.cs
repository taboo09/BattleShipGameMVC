using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BattleShip.Models;
using Microsoft.AspNetCore.Http;
using BattleShip.Helper;

namespace BattleShip.Controllers
{
    public class HomeController : Controller
    {
        private const int ROWS = 10;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();

            // save the ships
            HttpContext.Session.SetInt32("BattleShipPlayer", 5);
            HttpContext.Session.SetInt32("Destroyer1Player", 4);
            HttpContext.Session.SetInt32("Destroyer2Player", 4);

            HttpContext.Session.SetInt32("BattleShipPC", 5);
            HttpContext.Session.SetInt32("Destroyer1PC", 4);
            HttpContext.Session.SetInt32("Destroyer2PC", 4);
            
            /// Setup
            /// 0 = empty
            /// 1 = Battleship (5 squares)
            /// 2 = Destroyer (4 squares)
            /// 3 = Destroyer (4 squares)
            /// 4 = hit - empty
            /// 5 = hit - ship

            var players = new Players()
            {
                Player = Matrix.Setup(ROWS),
                Computer = Matrix.Setup(ROWS)
            };

            HttpContext.Session.SetMatrixAsJson("Players", players);

            return View(players);
        }

        public IActionResult Fire(int x, int y)
        {
            var players = HttpContext.Session.GetMatrixFromJson<Players>("Players");

            // player
            var player_hit = players.Computer[x, y];
            players.Computer[x, y] = players.Computer[x, y] == 0 ? 4 : 5;            

            var message_player = CheckShips(player_hit, "PC");
            
            // check if all ships were sunk
            if (message_player.Length > 0 && message_player.Length < 16)
            {
                if (AllShipsSunk("PC")) return GameOver("Player");
            }

            // computer
            var coordinates = Coordinates.RandomCoordinates(players.Player);
            var computer_hit = players.Player[coordinates.Item1, coordinates.Item2];
            players.Player[coordinates.Item1, coordinates.Item2] = computer_hit == 0 ? 4 : 5;

            var message_computer = CheckShips(computer_hit, "Player");

            if (message_computer.Length > 0 && message_computer.Length < 16)
            {
                if (AllShipsSunk("Player")) return GameOver("PC");
            }

            // re-assign the matrices
            HttpContext.Session.SetMatrixAsJson("Players", players);

            ViewBag.Ship = message_player;

            return View("Index", players);
        }

        private string CheckShips(int shipType, string player)
        {
            var message = "";

            switch (shipType)
            {
                case 1:
                    var battleship = HttpContext.Session.GetInt32($"BattleShip{player}") - 1;
                    message = battleship > 0 ? "Battleship has been hit" : "Battleship sunk";

                    HttpContext.Session.SetInt32($"BattleShip{player}", (int)battleship);
                    break;
                case 2:
                    var destroyer1 = HttpContext.Session.GetInt32($"Destroyer1{player}") - 1;
                    message = destroyer1 > 0 ? "Destroyer has been hit" : "Destoyer sunk";

                    HttpContext.Session.SetInt32($"Destroyer1{player}", (int)destroyer1);
                    break;
                case 3:
                    var destroyer2 = HttpContext.Session.GetInt32($"Destroyer2{player}") - 1;
                    message = destroyer2 > 0 ? "Destroyer has been hit" : "Destoyer sunk";

                    HttpContext.Session.SetInt32($"Destroyer2{player}", (int)destroyer2);
                    break;
                default:
                    break;
            }

            return message;
        }

        private bool AllShipsSunk(string player)
        {
            if (HttpContext.Session.GetInt32($"BattleShip{player}") > 0) return false;
            if (HttpContext.Session.GetInt32($"Destroyer1{player}") > 0) return false;
            if (HttpContext.Session.GetInt32($"Destroyer2{player}") > 0) return false;

            return true;
        }

        public IActionResult GameOver(string winner)
        {
            var message = winner == "PC" ? "You have lost the game!" : "You have won the game!";

            return View("../GameOver", message);
        }
    }
}
