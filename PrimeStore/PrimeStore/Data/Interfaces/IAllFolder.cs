using PrimeStore.Data.Models;

namespace PrimeStore.Data.Interfaces
{
    public interface IAllFolder
    {
        IEnumerable<Models.Folder> Folders { get; }
        public Folder Folder { set; }

    }
}
