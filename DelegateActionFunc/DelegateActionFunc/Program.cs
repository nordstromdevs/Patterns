using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateActionFunc
{
    class Program
    {
        static void Main(string[] args)
        {
            new DelegateTest().Run();

            Console.Write("\nPress any key to continue!\n");
            Console.ReadKey(true);

            new EventTest().Run();
            
            Console.Write("\nPress any key to quit!");
            Console.ReadKey(true);
        }
    }

    class DelegateTest
    {        
        private delegate int AddDelegate(int a, int b);

        public void Run()
        {
            AddDelegate myAddFunc = new AddDelegate(AddMethod);
            var result = myAddFunc(10, 5);
            Console.WriteLine("Result = " + result);

            var myAdd2Func = new Func<int, int, int>(AddMethod);
            var result2 = myAdd2Func(10, 5);
            Console.WriteLine("Result2 = " + result2);

            var myAdd3Func = new Func<int, int, int>((a, b) => a + b);
            var result3 = myAdd3Func(10, 5);
            Console.WriteLine("Result3 = " + result3);

            var mySubtractFunc = new Func<int, int, int>((a, b) => a - b);
            var result4 = CalcMethod(10, 5, mySubtractFunc);
            Console.WriteLine("Result4 = " + result4);

            var result5 = CalcMethod(10, 5, (a, b) => a - b);
            Console.WriteLine("Result5 = " + result5);

            var mySubtractAction = new Action<int, int>(SubtractMethod);
            mySubtractAction(10, 5);

            var mySubtractAction2 = new Action<int, int>((a, b) => Console.WriteLine("Result7 = " + (a - b)));
            mySubtractAction2(10, 5);
        }

        private int AddMethod(int a, int b)
        {
            return a + b;
        }

        private int CalcMethod(int a, int b, Func<int, int, int> func)
        {
            var result = func(a, b);
            return result;
        }

        private void SubtractMethod(int a, int b)
        {
            Console.WriteLine("Result6 = " + (a - b));
        }        
    }

    class EventTest
    {
        private event EventHandler<string> MyEvent;

        public EventTest()
        {            
            this.MyEvent += (sender, value) => Console.WriteLine("MyEvent: " + value);
        }

        public void Run()
        {           
            MyEvent?.Invoke(this, "Hello World!");
        }
    }
}
