using System.Reactive.Concurrency;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Acorisoft.Extensions.Platforms.Windows
{
    public static class Xaml
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static readonly object False= false;
        
        // ReSharper disable once MemberCanBePrivate.Global
        public static readonly object True= true;
        public static readonly Point ZeroPoint= new Point(0, 0);

        public static object Box(bool expression) => expression ? True : False;
    }
}