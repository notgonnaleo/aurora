using System.ComponentModel;
using Backend.Domain.Enums.EnumExtensions;

namespace Frontend.Web.Components.Settings.Theme
{
    public class ThemeSettings
    {
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
    }

    public enum Themes
    {
        [Description("Dark")]
        DarkMode = 0,
        [Description("Light")]
        LightMode = 1,
    }
}
