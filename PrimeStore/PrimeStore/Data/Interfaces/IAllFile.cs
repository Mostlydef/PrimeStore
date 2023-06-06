using PrimeStore.Data.Models;

namespace PrimeStore.Data.Interfaces
{
    public interface IAllFile
    {
        IEnumerable<Models.File> Files { get; } // добавить set

    }
}
