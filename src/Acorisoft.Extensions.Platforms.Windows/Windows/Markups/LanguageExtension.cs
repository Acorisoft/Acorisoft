using System;
using System.Windows.Markup;

namespace Acorisoft.Extensions.Platforms.Windows.Markups
{
    public class LanguageExtension : MarkupExtension
    {
        public LanguageExtension()
        {
            
        }

        public LanguageExtension(string key) => Key = key;
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Key;
        }
        
        public string Key { get; set; }
    }
}