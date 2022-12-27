using Store.Areas.Identity.Data;

namespace Store.Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<StoreUser> GetUsers();
        StoreUser GetUser(string id);
        StoreUser UpdateUser(StoreUser user);
    }
}
