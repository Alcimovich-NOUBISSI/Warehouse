using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Store.Areas.Identity.Data;

// Add profile data for application users by adding properties to the StoreUser class
public class StoreUser : IdentityUser
{
    [DisplayName("First Name")]
    public string FName { get; set; }
    [DisplayName("Last Name")]
    public string LName { get; set; }
}

public class StoreRoles : IdentityRole
{

}

