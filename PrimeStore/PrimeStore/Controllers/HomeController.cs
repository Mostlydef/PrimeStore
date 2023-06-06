using Microsoft.AspNetCore.Mvc;
using PrimeStore.Data.Interfaces;

namespace PrimeStore.Controllers
{
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
    }
}
