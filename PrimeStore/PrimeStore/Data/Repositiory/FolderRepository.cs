using Microsoft.EntityFrameworkCore;
using PrimeStore.Data.Interfaces;
using PrimeStore.Data.Models;

namespace PrimeStore.Data.Repositiory
{
    public class FolderRepository : IAllFolder
    {
        private readonly PrimeStoreContext _context;

        public FolderRepository(PrimeStoreContext context)
        {
            _context = context;
        }

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
    }
}
