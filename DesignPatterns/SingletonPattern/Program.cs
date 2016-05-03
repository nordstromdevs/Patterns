using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var item = Singleton.GetInstance();
            item.Description = "Hello World!";

            var item2 = Singleton.GetInstance();
            item2.Description = "Hello Sweden!";

            Console.WriteLine(item.Description);

            Console.WriteLine("\nPress any key to close!");
            Console.ReadKey();
        }
    }

    class Singleton
    {
        private static Singleton _instance;
        private static object _lockObj = new object();
        public string Description { get; set; }

        public static Singleton GetInstance()
        {
            lock(_lockObj)
            {
                if (_instance == null)
                    _instance = new Singleton();

                return _instance;
            }
        }        
    }
}
