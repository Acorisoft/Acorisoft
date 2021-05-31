using System;
using System.Globalization;
using System.Windows.Markup;
using Acorisoft.Extensions.Platforms.Windows.Converters;
using Acorisoft.Extensions.Platforms.Windows.Services;

namespace Acorisoft.Extensions.Platforms.Internals
{
    public class InternalLanguageExtension : MarkupExtension
    {
        public InternalLanguageExtension()
        {
            
        }

        public InternalLanguageExtension(string key) => Key = key;
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ServiceLocator.InternalLanguageService.GetString(Key, CultureInfo.CurrentCulture);
        }
        
        public string Key { get; set; }
    }
}