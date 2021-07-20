using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadasPOS.ViewModels
{
    public class PermissionViewModel
    {
        public string RoleId { get; set; }
        public List<RoleClaimsViewModel> RoleClaims { get; set; } = new();
    }
}
