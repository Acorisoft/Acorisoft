using System;
using System.Reactive;
using System.Reactive.Subjects;

namespace ConsoleApp1
{
    class Program
    {
        private static ISubject<int> _reply;
        private static ISubject<int> _behavior;
        private static ISubject<int> _subject;
        private static ISubject<int> _async;
        
        static void Main(string[] args)
        {
            
            _reply = new ReplaySubject<int>();
            _behavior = new BehaviorSubject<int>(0);
            _subject = new Subject<int>();
            _async = new AsyncSubject<int>();
            var observer = Observer.Create<int>(Console.WriteLine);
            var observers = new IObserver<int>[16];
            for (var i = 0; i < 16; i++)
            {
                observers[i]= Observer.Create<int>(Console.WriteLine);
            }
            
            _reply.OnNext(12);
            _behavior.OnNext(13);
            _subject.OnNext(14);
            _async.OnNext(15);

            _reply.Subscribe(observer);
            for (var i = 0; i < 16; i++)
            {
                _reply.Subscribe(observers[i]);
            }

            _behavior.Subscribe(observer);
            for (var i = 0; i < 16; i++)
            {
                _behavior.Subscribe(observers[i]);
            }
            _subject.Subscribe(observer);
            _async.Subscribe(observer);
            Console.WriteLine("Hello World!");
        }
        

    }
}