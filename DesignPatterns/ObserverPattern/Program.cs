using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var mySubject = new Subject();
            var myObserver = new Observer(mySubject);

            mySubject.SetText("Hello World!");
            mySubject.SetText("Hello Sweden!");

            Console.Write("\nPress any key to close!");
            Console.ReadKey();
        }
    }

    interface ISubject
    {
        void RegisterObserver(IObserver o);
        void UnregisterObserver(IObserver o);
        void NotifyObservers();
    }

    interface IObserver
    {
        void Update(string text);
    }

    class Subject : ISubject
    {
        List<IObserver> _observers;
        string _text;

        public Subject()
        {
            _observers = new List<IObserver>();
            _text = string.Empty;
        }

        public void RegisterObserver(IObserver o)
        {
            _observers.Add(o);
        }

        public void UnregisterObserver(IObserver o)
        {
            _observers.Remove(o);
        }

        public void NotifyObservers()
        {
            _observers.ForEach(x => x.Update(_text));
        }

        public void SetText(string text)
        {
            _text = text;
            NotifyObservers();
        }            
    }

    class Observer : IObserver
    {
        public Observer(ISubject subject)
        {
            subject.RegisterObserver(this);
        }

        public void Update(string text)
        {
            Console.WriteLine("Text updated: " + text);
        }
    }

}
