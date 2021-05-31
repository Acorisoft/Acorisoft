using System;
using System.Windows.Markup;
using Acorisoft.Extensions.Platforms.Languages;
using Splat;

namespace Acorisoft.Extensions.Platforms.Windows.Markups
{
    public class LanguageExtension : MarkupExtension
    {
        private static readonly Lazy<ILanguageService> LanguageInstance =
            new Lazy<ILanguageService>(() => Locator.Current.GetService<ILanguageService>());
        public LanguageExtension()
        {
            
        }

        public LanguageExtension(string key) => Key = key;
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return LanguageInstance.Value.GetString(Key);
        }
        
        public string Key { get; set; }
    }
}