using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using PrimeStore.Data.Interfaces;
using PrimeStore.Data.Models;
using System.Net.Mime;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpPost]
        public IActionResult DownloadFile(int id = -1)
        {
            if (id != -1)
            {
                var file = _iAllFile.GetFileData(id);
                var fileProvider = new FileExtensionContentTypeProvider();
                string filePath = Path.Combine(Syroot.Windows.IO.KnownFolders.Downloads.Path,
                file.Filename);
                var memoryStream = new MemoryStream(file.Data);
                fileProvider.TryGetContentType(file.Filename, out string contentType);
                return File(memoryStream, contentType, Path.GetFileName(filePath));
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
