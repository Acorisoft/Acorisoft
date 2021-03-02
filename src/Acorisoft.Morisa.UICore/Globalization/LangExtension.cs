using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Acorisoft.Morisa.Globalization
{
    public class LangExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }

    public static class LangExtensionMixin
    {
        internal static ResourceManager resMgr;
        public static IApplicationEnvironment UseGlobalization(this IApplicationEnvironment appEnv, ResourceManager mgr)
        {
            resMgr = mgr;

            return appEnv;
        }
    }
}
