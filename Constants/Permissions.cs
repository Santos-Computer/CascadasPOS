using CascadasPOS.Enums;
using CascadasPOS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CascadasPOS.Constants
{
    public static class Permissions
    {
        public static class Product
        {
            public const string View = "Permissions.Product.View";
            public const string Create = "Permissions.Product.Create";
            public const string Edit = "Permissions.Product.Edit";
            public const string Delete = "Permissions.Product.Delete";
        }

        public static class Category
        {
            public const string View = "Permissions.Category.View";
            public const string Create = "Permissions.Category.Create";
            public const string Edit = "Permissions.Category.Edit";
            public const string Delete = "Permissions.Category.Delete";
        }

        public static class Tax
        {
            public const string View = "Permissions.Tax.View";
            public const string Create = "Permissions.Tax.Create";
            public const string Edit = "Permissions.Tax.Edit";
            public const string Delete = "Permissions.Tax.Delete";
        }

        public static class PaymentType
        {
            public const string View = "Permissions.PaymentType.View";
            public const string Create = "Permissions.PaymentType.Create";
            public const string Edit = "Permissions.PaymentType.Edit";
            public const string Delete = "Permissions.PaymentType.Delete";
        }

        public static class Sales
        {
            public const string View = "Permissions.Sales.View";
            public const string Create = "Permissions.Sales.Create";
            public const string Edit = "Permissions.Sales.Edit";
            public const string Delete = "Permissions.Sales.Delete";
        }
        // Use in the user seed
        public static List<string> GeneratePermissionsForModule()
        {
            var modules = Enum.GetNames(typeof(Modules));
            List<string> allModules = new();

            var asm = Assembly.Load("CascadasPOS");
            var classes = asm.GetTypes().Where(p =>
                 p.Namespace == "CascadasPOS.Constants" && p.Name != "Permissions" && p.Name != "<>c"
            ).ToList();

            foreach (var type in classes)
            {
                FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

                foreach (FieldInfo fi in fields)
                {
                    allModules.Add(fi.GetValue(null).ToString());
                }
            }

            return allModules;
        }
    }
}
