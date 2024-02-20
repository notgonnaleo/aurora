
namespace Backend.Infrastructure.Enums.Localization
{
    public enum LanguagesEnum
    {
        English,
        Portuguese
    }
    public static class Localization
    {

        /// <summary>
        /// Generic error messages which can be re-used in different parts of the system.
        /// </summary>
        public static class GenericValidations
        {
            public static string ErrorSaveItem(LanguagesEnum language)
            {
                string response = "";
                switch (language)
                {
                    case LanguagesEnum.English:
                        response = "Couldn't save this item!";
                        break;
                    case LanguagesEnum.Portuguese:
                        response = "Não foi possível salvar o item!";
                        break;
                }
                return response;
            }
            public static string ErrorDeleteItem(LanguagesEnum language)
            {
                string response = "";
                switch (language)
                {
                    case LanguagesEnum.English:
                        response = "Couldn't delete this item!";
                        break;
                    case LanguagesEnum.Portuguese:
                        response = "Não foi possível deletar o item!";
                        break;
                }   
                return response;
            }
            public static string ErrorWrongTenant(LanguagesEnum language)
            {
                string response = "";
                switch (language)
                {
                    case LanguagesEnum.English:
                        response = "This item is not from the current tenant, please change to the correct tenant.";
                        break;
                    case LanguagesEnum.Portuguese:
                        response = "Este item não é do inquilino atual, por favor troque para o inquilino correto.";
                        break;
                }   
                return response;
            }

            public static string ErrorItemNotFound(LanguagesEnum language)
            {
                string response = "";
                switch (language)
                {
                    case LanguagesEnum.English:
                        response = "Couldn't find item!";
                        break;
                    case LanguagesEnum.Portuguese:
                        response = "Item não encontrado!";
                        break;
                }   
                return response;
            }
        }

        /// <summary>
        /// Error messages which will be used specifically for the Products module
        /// </summary>
        public static class ProductValidations
        {
            public static string ErrorProductNegativeValue(LanguagesEnum language)
            {
                string response = "";
                switch (language)
                {
                    case LanguagesEnum.English:
                        response = "Product value shouldn't be less than 0!";
                        break;
                    case LanguagesEnum.Portuguese:
                        response = "Valor do produto deve ser maior que 0!";
                        break;
                }
                return response;
            }
        }

        /// <summary>
        /// Error messages which will be used specifically for the Categories module
        /// </summary>
        public static class CategoryValidations
        {
            public static string ErrorCategoryMissingName(LanguagesEnum language)
            {
                string response = "";
                switch (language)
                {
                    case LanguagesEnum.English:
                        response = "Cannot create or update a category without specifying a name.";
                        break;
                    case LanguagesEnum.Portuguese:
                        response = "Não foi possível criar ou atualizar a categoria sem especificar um nome.";
                        break;
                }
                return response;
            }
        }

        /// <summary>
        /// Error messages which will be used specifically for the Sub-Categories module
        /// </summary>
        public static class SubCategoryValidations
        {
            public static string ErrorSubCategoryMissingParentCategory(LanguagesEnum language)
            {
                string response = "";
                switch (language)
                {
                    case LanguagesEnum.English:
                        response = "Cannot create a sub-category without specifying a parent category.";
                        break;
                    case LanguagesEnum.Portuguese:
                        response = "Não foi possível criar uma sub-categoria sem especificar uma categoria-pai.";
                        break;
                }
                return response;
            }
        }
    }
}