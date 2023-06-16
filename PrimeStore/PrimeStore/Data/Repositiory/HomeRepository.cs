using Microsoft.EntityFrameworkCore;
using PrimeStore.Data.Interfaces;
using PrimeStore.Data.Models;

namespace PrimeStore.Data.Repositiory
{
    public class HomeRepository : IAllFile, IAllFolder
    {
        private readonly PrimeStoreContext _context;

        public HomeRepository(PrimeStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.File> Files => _context.Files.Include(c => c.Folder);

        public IEnumerable<Folder> Folders
        {
            get
            {
                return _context.Folders.Include(c => c.User);
            }
        }

        public Folder Folder
        {
            set
            {
                _context.Folders.Add(value);
                _context.SaveChanges();
            }
        }

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
    }
}
