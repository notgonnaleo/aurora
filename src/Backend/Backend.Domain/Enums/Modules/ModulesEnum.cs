using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Enums.Modules
{
    /// <summary>
    /// Modules integer identification
    /// </summary>
    public enum ModulesEnum
    {
        Products = 1,
        ProductTypes = 2,
        Category = 3,
        SubCategory = 4,
        Agents = 5 
    }

    /// <summary>
    /// Endpoint module map
    /// </summary>
    public class Endpoints
    {
        public static string Authentication = "Authentication";
        public static string Tenant = "Tenant";
        public static string Membership = "Membership";
        public static string Products = "Products";
        public static string ProductsTypes = "ProductsTypes";
        public static string ProductVariants = "ProductVariants";
        public static string Category = "Categories";
        public static string SubCategory = "SubCategories";
        public static string Agents = "Agents";
        public static string AgentTypes = "AgentTypes";
        public static string Profiles = "Profiles";
        public static string Phones = "Phones";
        public static string Addresses = "Addresses";
        public static string EmailAddresses = "EmailAddresses";
        public static string Stock = "Stock";
        public static string Orders = "Orders";
    }

    /// <summary>
    /// Endpoint methods map
    /// </summary>
    public class Methods
    {
        /// <summary>
        /// Endpoint default methods - Usable for all modules
        /// </summary>
        public class Default
        {
            public static string GET = "List";
            public static string FIND = "Find";
            public static string POST = "Add";
            public static string PUT = "Update";
            public static string DELETE = "Delete";
        }

        // Below here we add our custom routes if necessary.
        // Otherwise, we use the generic ones which increase the productivity a little more when integrating it with the UI

        #region Authorization and Authentication
        /// <summary>
        /// Authentication custom endpoints methods
        /// </summary>
        public class Authentication
        {
            public static string Login = "Login";
            public static string Validate = "Validate";
            public static string SetTenant = "SetTenant";

            public static class SetTenantParameters
            {
                public static string tenantId = "tenantId";
            }
        }

        /// <summary>
        /// Tenants custom endpoints methods
        /// </summary>
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
        #endregion

        #region Products
        /// <summary>
        /// Products custom endpoints methods
        /// </summary>
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

            public static class PUT
            {
                public static class UpdateProduct
                {
                    public static string productId = "productId";
                    public static string tenantId = "tenantId";
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

        /// <summary>
        /// Product types custom endpoint methods
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public static class ProductVariants
        {
            public static class GET
            {
                public static string tenantId = "tenantId";
                public static string productId = "productId";
                public static string variantId = "variantId";

                public static class GetAllVariantsByProduct
                {
                    public static string GetAllVariantsByProductEndpoint = "GetAllVariantsByProduct";
                }


            }

            public static class POST
            {
                public static string AddProductVariant = "ProductVariant/Add";
            }

            public static class PUT
            {
                public static string UpdateProductVariant = "ProductVariant/Update";
            }

            public static class DELETE
            {
                public static class DeleteVariant
                {
                    public static string tenantId = "tenantId";
                    public static string variantId = "variantId";
                }
                
            }
        }

        /// <summary>
        ///  Categories custom endpoint methods
        /// </summary>
        public class Categories
        {
            public static class GET
            {
                public static class GetCategories
                {

                    public static string tenantId = "tenantId";
                }

                public static class GetCategory
                {
                    public static string tenantId = "tenantId";
                    public static string categoryId = "categoryId";
                }

                public static string GetCategoryAndSubCategories = "GetCategoryAndSubCategories";
                public static class GetCategoryAndSubCategoriesParameters
                {
                    public static string tenantId = "tenantId";
                }
            }

            public static class POST
            {
                public static string AddCategory = "Categories/Add";
            }

            public static class PUT
            {
                public static string UpdateCategory = "Categories/Update";
            }

            public static class DELETE
            {
                public static string tenantId = "tenantId";
                public static string categoryId = "categoryId";

                public static string DeleteCategory = "Categories/Delete";
            }
        }

        /// <summary>
        /// Sub-Categories custom endpoint methods
        /// </summary>
        public class SubCategories
        {
            public static class GET
            {
                public static string tenantId = "tenantId";
            }

            public static string GetSubCategoriesByCategory = "GetSubCategoriesByCategory";
            public static class GetSubCategoriesByCategoryParameters
            {
                public static string tenantId = "tenantId";
                public static string categoryId = "categoryId";
            }

            public static class POST
            {
                public static string AddSubCategory = "SubCategories/Add";
            }

            public static class PUT
            {
                public static string UpdateSubCategory = "SubCategories/Update";
            }

            public static class DELETE
            {
                public static string DeleteSubCategory = "SubCategories/Delete";
                public static class DeleteParameters
                {
                    public static string tenantId = "tenantId";
                    public static string categoryId = "categoryId";
                    public static string subCategoryId = "subCategoryId";
                }
            }
        }
        #endregion

        #region Agents
        /// <summary>
        /// Agents custom endpoint methods
        /// </summary>
        public class Agents
        {
            public static class GET
            {
                public static class GetAgentWithDetail
                {
                    public static string RouteName = "GetAgentWithDetail";
                    public static class Args
                    {
                        public static string agentId = "agentId";
                    }
                }
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
                    public static string agentId = "agentId";
                    public static string tenantId = "tenantId";
                }
            }

            public static class DELETE
            {
                public static class DeleteAgent
                {
                    public static string agentId = "agentId";
                    public static string tenantId = "tenantId";
                }
            }
        }

        public class Addresses
        {
            public static class GET
            {
                public static class GetAddresses
                {
                    public static string tenantId = "tenantId";
                }

                public static class GetAddress
                {
                    public static string tenantId = "tenantId";
                    public static string addressId = "addressId";
                }
            }

            public static class POST
            {
                public static class AddAddress
                {
                    public static string tenantId = "tenantId";
                }
            }

            public static class PUT
            {
                public static class UpdateAddress
                {
                    public static string addressId = "addressId";
                    public static string tenantId = "tenantId";
                }
            }

            public static class DELETE
            {
                public static class DeleteAddress
                {
                    public static string tenantId = "tenantId";
                    public static string addressId = "addressId";
                    
                }
            }
        }

        public class EmailAddresses
        {
            public static class GET
            {
                public static class GetEmails
                {
                    public static string tenantId = "tenantId";
                }

                public static class GetEmail
                {
                    public static string tenantId = "tenantId";
                    public static string emailAddressId = "emailAddressId";
                }
            }

            public static class POST
            {
                public static class AddEmail
                {
                    public static string tenantId = "tenantId";
                }
            }

            public static class PUT
            {
                public static class UpdateEmail
                {
                    public static string emailAddressId = "emailAddressId";
                    public static string tenantId = "tenantId";
                }
            }

            public static class DELETE
            {
                public static class DeleteEmail
                {
                    public static string emailAddressId = "emailAddressId";
                    public static string tenantId = "tenantId";
                }
            }
        }

        public class Phones
        {
            public static class GET
            {
                public static class GetPhones
                {
                    public static string tenantId = "tenantId";
                }

                public static class GetPhone
                {
                    public static string tenantId = "tenantId";
                    public static string phoneId = "phoneId";
                }
            }

            public static class POST
            {
                public static class AddPhone
                {
                    public static string tenantId = "tenantId";
                }
            }

            public static class PUT
            {
                public static class UpdatePhone
                {
                    public static string phoneId = "phoneId";
                    public static string tenantId = "tenantId";
                }
            }

            public static class DELETE
            {
                public static class DeletePhone
                {
                    public static string phoneId = "phoneId";
                    public static string tenantId = "tenantId";
                }
            }
        }

        public class Profiles
        {
            public static class GET
            {
                public static class GetProfiles
                {
                    public static string tenantId = "tenantId";
                }

                public static class GetProfile
                {
                    public static string tenantId = "tenantId";
                    public static string profileId = "profileId";
                }
            }

            public static class POST
            {
                public static class AddProfile
                {
                    public static string tenantId = "tenantId";
                }
            }

            public static class PUT
            {
                public static class SetAgentProfile
                {
                    public static string EndpointName = "SetAgentProfile";

                }

                public static class UpdateProfile
                {
                    public static string profileId = "profileId";
                    public static string tenantId = "tenantId";
                }

            }

            public static class DELETE
            {
                public static class DeleteProfile
                {
                    public static string profileId = "profileId";
                    public static string tenantId = "tenantId";
                }
            }
        }
        #endregion

        #region Stocks
        public class Stock
        {
            public static class GET
            {
                public static class GetStocks
                {
                    public static string tenantId = "tenantId";
                }
                public static class GetStock
                {
                    public static string tenantId = "tenantId";
                    public static string stockMovementId = "StockMovementId";
                }
                public static class GetStockWithDetail
                {
                    public static string GetStockWithDetailEndpoint = "GetStockWithDetail";
                    public static class Args
                    {
                        public static string tenantId = "tenantId";
                    }
                }
                public static class GetInventory
                {
                    public static string GetInventoryEndpointName = "GetInventory";
                    public static class Args
                    {
                        public static string tenantId = "tenantId";
                    }
                }
                public static class GetProductStock
                {
                    public static string GetProductStockEndpointName = "GetProductStock";
                    public static class Args
                    {
                        public static string tenantId = "tenantId";
                        public static string productId = "productId";
                        public static string variantId = "variantId";
                    }
                }
                public static class GetStockEntriesByProduct
                {
                    public static string GetStockEntriesByProductEndpointName = "GetStockEntriesByProduct";
                    public static class Args
                    {
                        public static string tenantId = "tenantId";
                        public static string productId = "productId";
                    }
                }
            }
            public static class POST
            {
                public static class AddStock
                {
                    public static string tenantId = "tenantId";
                }
            }
            public static class PUT
            {
                public static class UpdateStock
                {
                    public static string StockMovementId = "StockMovementId";
                    public static string tenantId = "tenantId";
                }
            }
            public static class DELETE
            {
                public static class DeleteStock
                {
                    public static string StockMovementId = "StockMovementId";
                    public static string tenantId = "tenantId";
                }
            }
        }
        #endregion

        public class Orders
        {
            public static class GET
            {
                public static class GetOrders
                {
                    public static string GetOrdersEndpointName = "GetOrders";
                    public static class Args
                    {
                        public static string tenantId = "tenantId";
                    }
                }
                public static class GetOrder
                {
                    public static string GetOrderEndpointName = "GetOrder";
                    public static class Args
                    {
                        public static string tenantId = "tenantId";
                        public static string orderId = "orderId";
                        public static string orderCode = "orderCode";
                    }
                }
            }
            public static class POST
            {
                public static class OpenNewOrder
                {
                    public static string OpenNewOrderEndpointName = "OpenNewOrder";
                }
            }
        }
    }
}
