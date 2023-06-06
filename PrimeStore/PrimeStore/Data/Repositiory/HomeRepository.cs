using Microsoft.EntityFrameworkCore;
using PrimeStore.Data.Interfaces;

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
    }
}
