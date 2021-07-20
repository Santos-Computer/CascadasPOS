using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadasPOS.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public IList<UserRolesViewModel> UserRoles { get; set; }
    }
}
