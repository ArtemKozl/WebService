
namespace WebApp.DataAccess.Repositories
{
    public interface IMessagesRepository
    {
        Task Create(int serialNumberDb, string messsageDb);
        Task<object> GetLastMessagesForTenMin();
    }
}