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
        public class Default // fix this ugly ass shit
        {
            public static string GET = "List";
            public static string FIND = "Find";
            public static string POST = "Add";
            public static string PUT = "Update";
            public static string DELETE = "Delete";
        }

        public class Authentication
        {
            public static string Login = "Login";
        }

        public class Products
        {
            public static class GET
            {
                public static class GetProducts
                {
                    public static string tenantId = "tenantId";
                }
                public static class GetProduct
                {
                    public static string tenantId = "tenantId";
                    public static string productId = "productId";
                }
            }

            public static class DELETE
            { 
                public static class DeleteProduct
                {
                    public static string tenantId = "tenantId";
                    public static string productId = "productId";
                }
            }
        }

        public static class ProductTypes 
        {

        }
    }

}
