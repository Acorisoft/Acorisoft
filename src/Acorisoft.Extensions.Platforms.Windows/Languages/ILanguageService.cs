using System;
using System.Globalization;
using System.Resources;

namespace Acorisoft.Extensions.Platforms.Languages
{
    public interface ILanguageService
    {
        string GetString(string key);
        string GetString(string key, CultureInfo culture);
    }

    public class LanguageService : ILanguageService
    {
        private readonly ResourceManager _resx;
        public LanguageService(ResourceManager resx)
        {
            _resx = resx ?? throw new ArgumentNullException(nameof(resx));
        }

        public string GetString(string key)
        {
            return GetString(key, CultureInfo.CurrentCulture);
        }

        public string GetString(string key, CultureInfo culture)
        {
            return _resx.GetString(key, culture);
        }
    }
}