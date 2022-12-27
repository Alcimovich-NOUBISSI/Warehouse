using Microsoft.AspNetCore.Identity;
using Store.Areas.Identity.Data;
using Store.Data;
using Store.Data.Repositories;

namespace Store.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public readonly IdentityContext _context;

        public RoleRepository(IdentityContext context)
        {
            _context = context;
        }
        public IEnumerable<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
