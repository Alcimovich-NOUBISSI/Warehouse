using Store.Areas.Identity.Data;
using Store.Data;
using Store.Data.Repositories;

namespace Store.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityContext _context;

        public UserRepository(IdentityContext context)
        {
            _context = context;
        }

        public IEnumerable<StoreUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public StoreUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }

        public StoreUser UpdateUser(StoreUser user)
        {
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }
    }
}
