using CascadasPOS.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CascadasPOS.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetPermissions(this List<RoleClaimsViewModel> allPermissions, string filterModule)
        {
            var asm = Assembly.Load("CascadasPOS");
            var classes = asm.GetTypes().Where(p =>
                 p.Namespace == "CascadasPOS.Constants" && p.Name != "Permissions" && p.Name != "<>c"
            ).ToList();

            if (string.IsNullOrEmpty(filterModule) == false && filterModule != "All")
            {
                classes = classes.Where(x => x.Name == filterModule).ToList();
            }

            foreach (var type in classes)
            {
                FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (FieldInfo fi in fields)
                {
                    allPermissions.Add(new RoleClaimsViewModel { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
                }
            }

        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
