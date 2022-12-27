using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Areas.Identity.Data;

namespace Store.Models.DataViewModels
{
    public class EditUserViewModel
    {
        public StoreUser User { get; set; }
        public IList<SelectListItem> Roles { get; set; }

    }
}
