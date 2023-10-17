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

    public class Endpoints
    {
        public static string Authentication = "Authentication";
        public static string Products = "Products";
    }

    public class Methods
    {
        public static string GET = "List";
        public static string POST = "Add";
        public static string PUT = "Update";
        public static string DELETE = "Delete";
    }

}
