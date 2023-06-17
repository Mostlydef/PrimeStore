using Microsoft.EntityFrameworkCore;
using PrimeStore.Data.Interfaces;
using PrimeStore.Data.Models;

namespace PrimeStore.Data.Repositiory
{
    public class HomeRepository : IAllFile
    {
        private readonly PrimeStoreContext _context;

        public HomeRepository(PrimeStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.File> Files => _context.Files.Include(c => c.Folder);

        public Models.File File
        {
            set
            {
                _context.Files.Add(value);
                _context.SaveChanges();
            }
        }

        public bool SetFileInBasket(int id)
        {
            Models.File file = _context.Files.FirstOrDefault(p => p.Id == id);
            if(file != null)
            {
                file.InBasket = true;
                _context.Files.Update(file);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveFileFromBasket(int id)
        {
            Models.File file = _context.Files.FirstOrDefault(p => p.Id == id);
            if (file != null)
            {
                file.InBasket = false;
                _context.Files.Update(file);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveFile(int id)
        {
            Models.File file = _context.Files.FirstOrDefault(p => p.Id == id);
            if (file != null)
            {
                _context.Files.Remove(file);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Models.File GetFileData(int id)
        {
            Models.File file = _context.Files.FirstOrDefault(p => p.Id == id);
            return file;
        }
    }
}
