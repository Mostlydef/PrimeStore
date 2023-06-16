using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Data.Interfaces;

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
    }
}
