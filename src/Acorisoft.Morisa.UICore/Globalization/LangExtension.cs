using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Acorisoft.Morisa.Globalization
{
    public class LangExtension : MarkupExtension
    {
        private const string Untitled= "Untitled";

        public LangExtension()
        {

        }

        public LangExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Key))
            {
                return Untitled;
            }

            return LangExtensionMixin.resMgr.GetString(Key, LangExtensionMixin.current);
        }

        public string Key { get; set; }
    }

    public static class LangExtensionMixin
    {
        internal static ResourceManager resMgr;
        internal static CultureInfo current;

        public static IApplicationEnvironment UseGlobalization(this IApplicationEnvironment appEnv, ResourceManager mgr,CultureInfo info)
        {
            resMgr = mgr;
            current = info;
            return appEnv;
        }
    }
}
