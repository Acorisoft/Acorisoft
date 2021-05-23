using System;
using System.Collections.Concurrent;
using System.Windows.Threading;

namespace Acorisoft.Extensions.Platforms.Windows.Threadings
{
    
    /// <summary>
    /// <see cref="DispatcherTimerFactory"/> 表示一个调度计时器工厂类。
    /// </summary>
    public static class DispatcherTimerFactory
    {
         private static readonly ConcurrentBag<DispatcherTimer> _timers;

        static DispatcherTimerFactory()
        {
            _timers = new ConcurrentBag<DispatcherTimer>();
        }

        public static void StopAll()
        {
            foreach (var timer in _timers)
            {
                timer?.Stop();
            }
        }
        
         /// <summary>
        ///     Creates a timer that uses the current thread's Dispatcher to
        ///     process the timer event at background priority.
        /// </summary>
        public static DispatcherTimer Create()
        {
            var instance = new DispatcherTimer();
            _timers.Add(instance);
            return instance;
        }
        
        /// <summary>
        ///     Creates a timer that uses the current thread's Dispatcher to
        ///     process the timer event at the specified priority.
        /// </summary>
        /// <param name="priority">
        ///     The priority to process the timer at.
        /// </param>
        public static DispatcherTimer Create(DispatcherPriority priority) // NOTE: should be Priority
        {
            
            var instance = new DispatcherTimer(priority);
            _timers.Add(instance);
            return instance;
        }
        
        /// <summary>
        ///     Creates a timer that uses the specified Dispatcher to
        ///     process the timer event at the specified priority.
        /// </summary>
        /// <param name="priority">
        ///     The priority to process the timer at.
        /// </param>
        /// <param name="dispatcher">
        ///     The dispatcher to use to process the timer.
        /// </param>
        public static DispatcherTimer Create(DispatcherPriority priority, Dispatcher dispatcher)  // NOTE: should be Priority
        {
            if(dispatcher == null)
            {
                throw new ArgumentNullException("dispatcher");
            }

            var instance = new DispatcherTimer(priority, dispatcher);
            _timers.Add(instance);
            return instance;
        }
        
        public static DispatcherTimer Create(TimeSpan interval, DispatcherPriority priority, EventHandler callback, Dispatcher dispatcher)
        {
            if(callback == null)
            {
                throw new ArgumentNullException("callback");
            }
            if(dispatcher == null)
            {
                throw new ArgumentNullException("dispatcher");
            }

            if (interval.TotalMilliseconds < 0)
                throw new ArgumentOutOfRangeException("interval");

            if (interval.TotalMilliseconds > Int32.MaxValue)
                throw new ArgumentOutOfRangeException("interval");

            var instance = new DispatcherTimer(interval, priority, callback, dispatcher);
            _timers.Add(instance);
            return instance;
        }
    }
}