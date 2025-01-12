namespace PrimeStore.Data.Interfaces
{
    public interface IUser
    {
        IEnumerable<Models.User> Users { get; }
    }
}
