
namespace Backend.Infrastructure.Enums.Localization
{
    public enum LanguagesEnum
    {
        English,
        Portuguese
    }
    public static class Localization
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
}