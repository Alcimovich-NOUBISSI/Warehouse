using Store.Data.Repositories;

namespace Store.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public UnitOfWork(IUserRepository users, IRoleRepository roles)
        {
            Users = users;
            Roles = roles;
        }
    }
}
