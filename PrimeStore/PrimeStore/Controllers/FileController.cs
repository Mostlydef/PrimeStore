using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeStore.Data.Interfaces;
using PrimeStore.ViewModels;
using PrimeStore.Data.Models;
using System.IO;
using Npgsql.PostgresTypes;
using Npgsql;

namespace PrimeStore.Controllers
{
    public class FileController : Controller
    {

        private readonly IAllFile _allFile;
        private readonly IAllFolder _allFolder;

        public FileController(IAllFile allFile, IAllFolder allFolder)
        {
            _allFile = allFile;
            _allFolder = allFolder;
        }

        public async Task<IActionResult> AddFile(FileViewModel fileModel)
        {
            if (fileModel.FormFile != null)
            {
                byte[] buffer = null;
                using (var binaryReader = new BinaryReader(fileModel.FormFile.OpenReadStream()))
                {
                    buffer = binaryReader.ReadBytes((int)fileModel.FormFile.Length);
                    _allFile.File = new Data.Models.File
                    {
                        Data = buffer,
                        Filename = fileModel.FormFile.FileName,
                        UploadTime = DateTime.Now,
                        FolderId = (int)(_allFolder.Folders.FirstOrDefault()?.Id),
                        Size = fileModel.FormFile.Length.ToString(),
                        InBasket = false
                    };
                }
                return RedirectToAction("Index", "Home");
            }
            return View(fileModel);
        }

        public ViewResult Basket()
        {
            var allFile = _allFile.Files;
            return View(allFile);
        }

        [HttpPost]
        public IActionResult RemoveFileFromBasket(int id = -1)
        {
            if (id != -1)
            {
                _allFile.RemoveFileFromBasket(id);
            }
            return RedirectToAction("Basket", "File");
        }

        [HttpPost]
        public IActionResult RemoveFile(int id = -1)
        {
            if (id != -1)
            {
                _allFile.RemoveFile(id);
            }
            return RedirectToAction("Basket", "File");
        }
    }
}
