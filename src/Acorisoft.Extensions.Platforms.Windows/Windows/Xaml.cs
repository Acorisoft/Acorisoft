using System.Reactive.Concurrency;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Acorisoft.Extensions.Platforms.Windows
{
    public static class Xaml
    {
        static Xaml()
        {
            False = false;
            True = true;
            ZeroPoint = new Point(0, 0);
            MainThreadScheduler = new SynchronizationContextScheduler(SynchronizationContext.Current ?? new DispatcherSynchronizationContext());
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        public static readonly object False;
        
        // ReSharper disable once MemberCanBePrivate.Global
        public static readonly object True;
        public static readonly Point ZeroPoint;

        public static object Box(bool expression) => expression ? True : False;

        public static IScheduler MainThreadScheduler { get; }
    }
}