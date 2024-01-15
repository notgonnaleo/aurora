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
        ProductTypes = 2,
        Category = 3,
        SubCategory = 4,
        Agents = 5 
    }

    public class Endpoints
    {
        public static string Authentication = "Authentication";
        public static string Tenant = "Tenant";
        public static string Membership = "Membership";
        public static string Products = "Products";
        public static string ProductsTypes = "ProductsTypes";
        public static string Category = "Categories";
        public static string SubCategory = "SubCategories";
        public static string Agents = "Agents";
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

        public class Tenant
        {
            public static string GetTenantsByUserId = "GetTenantsByUserId";
            public static class GetTenantsByUserIdParameters
            {
                public static string userId = "userId";
            }
            public static class GetTenantById
            {
                public static string tenantId = "Id";

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

        public class ProductTypes
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

        public class Categories
        {
            public static class GET
            {
                public static string GetAllCategories = "Categories/List";
                public static string GetCategory = "Categories/Find";
            }

            public static class POST
            {
                public static string AddCategory = "Categories/Add";
            }

            public static class PUT
            {
                public static string UpdateCategory = "Categories/Update";
            }
        }

        public class SubCategories
        {
            public static class GET
            {
                public static string GetAllSubCategories = "SubCategories/List";
                public static string GetSubCategory = "SubCategories/Find";
            }

            public static class POST
            {
                public static string AddSubCategory = "SubCategories/Add";
            }

            public static class PUT
            {
                public static string UpdateSubCategory = "SubCategories/Update";
            }
        }

        public class Agents
        {
            public static class GET
            {
                public static class GetAgents
                {
                    public static string tenantId = "tenantId";
                }
                public static class GetAgent
                {
                    public static string tenantId = "tenantId";
                    public static string agentId = "agentId";
                }
            }

            public static class POST
            {
                public static class AddAgent
                {
                    public static string tenantId = "tenantId";
                }
            }

            public static class PUT
            {
                public static class UpdateAgent
                {
                    public static string Id = "Id";
                    public static string tenantId = "tenantId";
                }
            }

            public static class DELETE
            {
                public static class DeleteAgent
                {
                    public static string Id = "Id";
                    public static string tenantId = "tenantId";
                }
            }
        }

    }
}
