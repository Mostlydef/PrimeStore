using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Data.Interfaces;
using PrimeStore.Data.Models;

namespace PrimeStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAllFile _iAllFile;

        public HomeController(IAllFile iAllFile)
        {
            _iAllFile = iAllFile;
        }

        public ViewResult Index()
        {
            var allFile = _iAllFile.Files;
            return View(allFile);
        }

        [HttpPost]
        public IActionResult GetFileInBasket(int id = -1)
        {
            if (id != -1)
            {
                _iAllFile.SetFileInBasket(id);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
