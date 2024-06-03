using System.ComponentModel;
using System.Net;
using Backend.Domain.Enums.EnumExtensions;
using Frontend.Web.Util.Cookie;

namespace Frontend.Web.Components.Settings.Theme
{
    public class ThemeSettingsFeatureSet
    {
        private readonly CookieHandler _cookies;
        public ThemeSettingsFeatureSet(CookieHandler cookies)
        {
            _cookies = cookies;
        }
        public async void SaveTheme(int themeId)
        {
           await _cookies.SetValueAsync("theme", themeId.ToString());
        }
        public async Task<ThemeSettings> SetDefaultTheme()
        {
            await _cookies.SetValueAsync("theme", "0");
            return new ThemeSettings()
            {
                ThemeId = (int)Themes.DarkMode,
                ThemeName = Themes.DarkMode.ToString()
            };
        }
        public async Task<ThemeSettings> GetUserTheme()
        {
            var theme = await _cookies.GetValueAsync<int?>("theme");
            if (theme is null || theme > 1)
                return await SetDefaultTheme();

            var userTheme = (Themes)theme;
            return new ThemeSettings()
            {
                ThemeId = (int)userTheme,
                ThemeName = userTheme.ToString()
            };
        }
        public bool ReturnThemeIdAsBoolean(int themeId)
        {
            return themeId == 1;
        }
    }

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
