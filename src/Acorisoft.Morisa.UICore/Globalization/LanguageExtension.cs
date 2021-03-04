using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Acorisoft.Morisa.Globalization
{
    public class LanguageExtension : MarkupExtension
    {
        private const string Untitled= "Untitled";

        public LanguageExtension()
        {

        }

        public LanguageExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Key))
            {
                return Untitled;
            }

            return GlobalizationExtension.GetString(Key);
        }

        public string Key { get; set; }
    }


}
