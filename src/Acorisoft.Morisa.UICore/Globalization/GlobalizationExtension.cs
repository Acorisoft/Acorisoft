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
    public static class GlobalizationExtension
    {
        private static ResourceManager _resMgr;
        private static CultureInfo _current;

        public static IApplicationEnvironment UseGlobalization(this IApplicationEnvironment appEnv, ResourceManager mgr, CultureInfo info)
        {
            _resMgr = mgr;
            _current = info;
            return appEnv;
        }

        public static CultureInfo Culture {
            get => _current;
            set => _current = value ?? throw new ArgumentNullException(nameof(value));
        }

#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        public static string? GetString(string key)
#pragma warning restore CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        {
            return _resMgr?.GetString(key, _current);
        }
    }
}
