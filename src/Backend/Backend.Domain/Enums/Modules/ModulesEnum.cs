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
        public static string ProductsTypes = "ProductsTypes";
        public static string Category = "Category";
        public static string SubCategory = "SubCategory";
    }

    public class Methods
    {
        public class Default
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
            public static string SetTenant = "SetTenant";

            public static class SetTenantParameters
            {
                public static string tenantId = "tenantId";
            }
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
            public static class GET
            {
                public static string GetAllProductTypes = "ProductTypes/List";

                public static class GetProductType
                {
                    public static string ProductTypeId = "ProductTypes/Find";
                }
            }

            public static class POST
            {
                public static string AddProductType = "ProductTypes/Add";
            }

            public static class PUT
            {
                public static string UpdateProductType = "ProductTypes/Update";
            }
        }

        public static class Categorys
        {
            public static class GET
            {
                public static string GetAllCategories = "Categorys/List";
                public static string GetCategory = "Categorys/Find";
            }

            public static class POST
            {
                public static string AddCategory = "Categorys/Add";
            }

            public static class PUT
            {
                public static string UpdateCategory = "Categorys/Update";
            }
        }

        public static class SubCategorys
        {
            public static class GET
            {
                public static string GetAllSubCategories = "SubCategorys/List";
                public static string GetSubCategory = "SubCategorys/Find";
            }

            public static class POST
            {
                public static string AddSubCategory = "SubCategorys/Add";
            }

            public static class PUT
            {
                public static string UpdateSubCategory = "SubCategorys/Update";
            }
        }

    }
}
