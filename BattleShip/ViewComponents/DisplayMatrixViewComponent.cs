using System.Threading.Tasks;
using BattleShip.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleShip.ViewComponents
{
    public class DisplayMatrixViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int[,] matrix, string player)
        {
            var viewModel = new ViewModel { Matrix = matrix, Player = player };

            return View<ViewModel>("../DisplayMatrix", viewModel);
        }
    }
}