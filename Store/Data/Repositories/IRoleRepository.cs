using Microsoft.AspNetCore.Identity;
using Store.Areas.Identity.Data;

namespace Store.Data.Repositories
{
    public interface IRoleRepository
    {
        public IEnumerable<IdentityRole> GetRoles();
    }
}
