using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Enums.Modules
{
    public enum ModulesEnum
    {
        Products = 1,
        ProductTypes = 2
    }

    public class ModulesEnumAlias
    {
        public static readonly string Authentication = "Authentication";
        public static readonly string Products = "Products";
    }

}
