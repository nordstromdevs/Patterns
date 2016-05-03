using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.IO.IsolatedStorage;

namespace Study
{
    class Program
    {
        private static void Main(string[] args)
        {
            //new LinqAndLanguageFeaturesCode().RunCode();
            //new QueryAndMethodCode().RunCode();
            //new IsolatedStorageCode().RunCode();
            //new EncryptionCode().RunCode();
            //new ValidationAndContractCode().RunCode();
            //new CancelActionCode().RunCode();
            //new SemaphoreCode().RunCode();
            //new MutexCode().RunCode();
            //new LockAndQueueCode().RunCode();
            //new AsyncAndAwaitCode().RunCode();
            //new ParallelTaskCode().RunCode();
            //new ThreadpoolCode().RunCode();
            //new EventCode().RunCode();
            new ReflectionCode().RunCode();
            //new DelegateCode().RunCode();
            //new GenericsCode().RunCode();
            //new ExtensionCode().RunCode();

            Console.Write("\nPress Any key to Continue!");
            Console.ReadKey();
        }

        #region "LinqAndLanguageFeatures"
        private class LinqAndLanguageFeaturesCode
        {
            public void RunCode()
            {
                AnonumousTypeCode();

                int? a = null;

                Func<int, bool> func = n => n > 3;

                if (func(1))
                    Console.WriteLine("1 is bigger than 3");
                else
                    Console.WriteLine("1 is not bigger than 3");


            }

            private void AnonumousTypeCode()
            {
                var myAnonumousTypedObj = new
                {
                    Id = 0,
                    Name = "Kent",
                    Age = 39
                };

                foreach (var prop in myAnonumousTypedObj.GetType().GetProperties())
                {
                    Console.WriteLine(prop.Name + " = " + prop.GetValue(myAnonumousTypedObj));
                }
            }

        }
        #endregion

        #region "Query and Method syntax"
        private class QueryAndMethodCode
        {
            public void RunCode()
            {
                var data = Enumerable.Range(1, 50);
                var method = data.Where(x => x % 2 == 0).Select(x => x.ToString());
                var query =
                    from d in data
                    where d % 2 == 0
                    select d.ToString();

                var projection =
                    from d in data
                    select new
                    {
                        Even = (d % 2 == 0),
                        Odd = !(d % 2 == 0),
                        Value = d,
                        Programmer = "Kent",
                        Age = 39
                    };

                foreach (var item in projection)
                {
                    Console.WriteLine("Value: {0}", item.Value);
                }

                var letters = new[] { "A", "C", "B", "E", "Q" };

                var sortAsc =
                    from d in data
                    orderby d ascending
                    select d;

                var sortDesc =
                    data.OrderByDescending(x => x);


                var values = new[] { "A", "B", "A", "C", "A", "D" };

                var distinct = values.Distinct();
                var first = values.First();
                var firstOr = values.FirstOrDefault();
                var last = values.Last();
                var page = values.Skip(2).Take(2);

                // aggregates

                var numbers = Enumerable.Range(1, 50);
                var any = numbers.Any(x => x % 2 == 0);
                var count = numbers.Count(x => x % 2 == 0);
                var sum = numbers.Sum();
                var max = numbers.Max();
                var min = numbers.Min();
                var avg = numbers.Average();

                var dictionary = new Dictionary<string, string>()
                {
                     {"1", "B"}, {"2", "A"}, {"3", "B"}, {"4", "A"},
                };

                var group =
                    from d1 in dictionary
                    group d1 by d1.Value into g
                    select new
                    {
                        Key = g.Key,
                        Members = g,
                    };

                // JOIN

                var dictionary1 = new Dictionary<string, string>()
                {
                     {"1", "B"}, {"2", "A"}, {"3", "B"}, {"4", "A"}, {"0", "C"}
                };

                var dictionary2 = new Dictionary<string, string>()
                {
                     {"5", "B"}, {"6", "A"}, {"7", "B"}, {"8", "A"},
                };

                var join =
                    from d1 in dictionary1
                    join d2 in dictionary2 on d1.Value equals d2.Value
                    select new
                    {
                        Key1 = d1.Key,
                        Key2 = d2.Key,
                        Value = d1.Value
                    };

                foreach (var item in join)
                {
                    Console.WriteLine("key1 {0}, key2 {1}, value {2}", item.Key1, item.Key2, item.Value);
                }


            }
        }
        #endregion

        // TODO Linq to XML

        // TODO LocalDB and SQL CE

        // TODO REST AND JSON

        #region "Isolated storage"
        private class IsolatedStorageCode
        {
            public void RunCode()
            {
                try
                {
                    var myStorage = IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly, "Study1");
                    var myFile = myStorage.CreateFile("MyIsolatedStorageFile.txt");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        #endregion

        // TODO DIAGNOSTICS - Create and use performance counters

        #region "Encryption"
        private class EncryptionCode
        {
            public void RunCode()
            {
                //FileEncrypt();
                Hashing();   // (SHA256)  
                // Windows Data Protection (only exists in .NET 4.0 and earier?)
                // Symmetric encryption (Rijndael and Aes)
                // Asymmetric encryption (Create Signature, our public key, private keys)
            }

            private void Hashing()
            {
                var password = Encoding.Unicode.GetBytes("P4ssw0rd!");
                var passwordHash = SHA256.Create().ComputeHash(password);
                Console.WriteLine("Password: {0}, Hash: {1}", Encoding.Unicode.GetString(password), passwordHash);
            }

            private void FileEncrypt()
            {
                var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyDataFile.txt");

                try
                {
                    File.WriteAllText(filename, "This is a bunch of super secret content!");
                    File.Encrypt(filename);
                }
                catch (Exception ex)
                {
                    /* NOTE from MSDN: This problem occurs if the EFS recovery policy that is implemented on the client computer contains one or more EFS recovery agent certificates that have expired. 
                     * Client computers cannot encrypt any new documents until a valid recovery agent certificate is available.*/
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        #endregion

        #region "Validation and Contract"
        private class ValidationAndContractCode
        {
            public void RunCode()
            {
                Animal cat = new Cat();
                Animal dog = new Dog();

                if (cat is Dog)
                    Console.WriteLine("cat is Dog - Validation error");

                if (cat is Animal)
                    Console.WriteLine("cat is Animal - Validation success");

                // test
                cat.SetName("Kisan");
                var name = cat.GetName();
                cat.SetName("");
                var name2 = cat.GetName();
                cat.Empty();
                var name3 = cat.GetName();
                var name4 = cat.Name;

                /* NOTE (from MSDN) You must use a binary rewriter to insert run-time 
                 * enforcement of contracts. Otherwise, contracts such as the Contract.
                 * Ensures method can only be tested statically and will not 
                 * throw exceptions during run time if a contract is violated.*/
            }

            private class Animal
            {
                public string Name { get; set; }

                public void SetName(string value)
                {
                    Contract.Requires(!string.IsNullOrWhiteSpace(value), "value is empty");
                    this.Name = value;
                }

                public string GetName()
                {
                    Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
                    return this.Name;
                }

                public void Empty()
                {
                    this.Name = "";
                }
            }

            private class Dog : Animal
            {
                public string BarkingSound { get; set; }
            }

            private class Cat : Animal
            {
                public string PurringSound { get; set; }
            }

        }
        #endregion

        #region "Cancel Action"
        private class CancelActionCode
        {
            public void RunCode()
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                Action<CancellationToken> action = async (token) =>
                {
                    for (var i = 0; i < 1000; i++)
                    {
                        Console.WriteLine("{0} {1}", i, stopwatch.Elapsed.TotalMilliseconds);
                        await Task.Delay(10);
                        if (token.IsCancellationRequested)
                            break;
                    }
                };

                var source = new CancellationTokenSource();
                Console.WriteLine("Start");
                action.Invoke(source.Token);
                Thread.Sleep(5000);
                source.Cancel();

                stopwatch.Stop();
            }
        }
        #endregion

        #region "Semaphore"
        private class SemaphoreCode
        {
            private SemaphoreSlim _semaphore;
            private Random _random;
            private object _syncObject;

            public void RunCode()
            {
                _semaphore = new SemaphoreSlim(5);
                _random = new Random();
                _syncObject = new object();

                for (var i = 0; i < 10; i++)
                {
                    var myThread = new Thread(StartDoormanService);
                    myThread.Start(i);
                }
            }

            private void StartDoormanService(object id)
            {
                WriteLine("{0} Request", id, ConsoleColor.Yellow);
                _semaphore.Wait();
                WriteLine("{0} Accept", id, ConsoleColor.Green);
                Thread.Sleep(_random.Next(2500, 10000));
                WriteLine("{0} Exited", id, ConsoleColor.Red);
                _semaphore.Release();
            }

            private void WriteLine(string formattedText, object id, ConsoleColor color = ConsoleColor.White)
            {
                lock (_syncObject) // lock needed as call can come between method lines below, resulting in wrong color
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine(formattedText, id);
                }
            }
        }
        #endregion

        #region "Mutex"
        private class MutexCode
        {
            private static readonly string _appId = "BBA4993A-B60F-4740-B2E9-6F1144E1DF91";

            public void RunCode()
            {
                using (var mutex = new Mutex(false, @"Global\" + _appId)) // The "Global\" prefix enforces only one instance of the app on terminal services too!
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        Console.WriteLine("Application already running!");
                        return; // TODO exit application
                    }
                }
            }
        }
        #endregion

        #region "Lock and Queue"
        private class LockAndQueueCode
        {
            private static readonly Queue<int> _workUnits = new Queue<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            public void RunCode()
            {
                var worker1 = new Worker(_workUnits, "Worker 1", 300);
                var worker2 = new Worker(_workUnits, "Worker 2", 1000);
                var worker3 = new Worker(_workUnits, "Worker 3", 100);

                Parallel.Invoke(worker1.DoWork, worker2.DoWork, worker3.DoWork);
            }

            private class Worker
            {
                private static readonly object _syncObject = new object();
                private readonly string _name;
                private readonly Queue<int> _queue;
                private readonly int _wait;

                public Worker(Queue<int> queue, string name, int wait)
                {
                    _queue = queue;
                    _name = name;
                    _wait = wait;
                }

                public void DoWork()
                {
                    while (_queue.Count > 0)
                    {
                        lock (_syncObject)
                        {
                            if (_queue.Count > 0) // different workers are working with the same Queue!
                            {
                                Console.WriteLine("{0}: Count remaining - {1}", _name, _queue.Count);
                                Thread.Sleep(_wait);
                                _queue.Dequeue();
                            }
                            else
                            {
                                Console.WriteLine("Phew! {0} dodged a bullet there!", _name);
                            }
                        }
                    }

                    Console.WriteLine("{0} finished", _name);
                }
            }
        }
        #endregion

        #region "Async and Await"

        private class AsyncAndAwaitCode
        {
            public void RunCode()
            {
                Action action = async () => {
                    RunWithoutAsync();

                    var result = await RunWithAsync();
                    Console.WriteLine("With Async - Finished > " + result);
                };

                Console.WriteLine("Being All");
                action.Invoke();
                Console.WriteLine("End All");
            }

            private void RunWithoutAsync()
            {
                var worker = new BackgroundWorker();
                worker.DoWork += (s, e) => {
                    Thread.Sleep(new Random().Next(3, 100));
                    e.Result = DateTime.Now;
                };
                worker.RunWorkerCompleted += (s, e) => {
                    Console.WriteLine("Without Async - Finished > " + e.Result);
                };

                Console.WriteLine("Without Async - Started");
                worker.RunWorkerAsync();
            }

            private async Task<DateTime> RunWithAsync()
            {
                Console.WriteLine("With Async - Started");
                await Task.Delay(new Random().Next(3, 100));
                return DateTime.Now;
            }
        }

        #endregion

        #region "Parallell Task"
        private class ParallelTaskCode
        {
            public void RunCode()
            {
                Parallel.Invoke(DoSomeCode, DoSomeMoreCode, () => Console.WriteLine("Last one - Finished"));
            }

            private void DoSomeCode()
            {
                Thread.Sleep(8000);
                Console.WriteLine("DoSomeCode - Finished");
            }

            private void DoSomeMoreCode()
            {
                Thread.Sleep(4000);
                Console.WriteLine("DoSomeMoreCode - Finished");
            }
        }
        #endregion

        #region "Threadpools"
        private class ThreadpoolCode
        {
            public void RunCode()
            {
                ThreadPool.SetMaxThreads(100, 1);
                ThreadPool.QueueUserWorkItem(QueueCallback, "One");

                // anonymous function with lambdas as callback (as an alternative)
                ThreadPool.QueueUserWorkItem((o) => {
                    Thread.Sleep(new Random().Next(3, 100));
                    Console.WriteLine("Hello Callback {0}", o.ToString());
                }, "Two");
            }

            private static void QueueCallback(object state)
            {
                Thread.Sleep(new Random().Next(3, 100));
                Console.WriteLine("Hello Callback {0}", state);
            }
        }
        #endregion

        #region "Events"
        private class EventCode
        {
            public void RunCode()
            {
                var dog = new Dog();
                dog.Barked += (m) => Console.WriteLine(m);

                dog.Bark("Hello Event");
            }

            private class Dog
            {
                public delegate void BarkedEventArgs(string message);
                public event BarkedEventArgs Barked;

                public void Bark(string message)
                {
                    if (Barked != null)
                        Barked(message);
                }
            }
        }

        #endregion

        #region "Reflection"
        private class ReflectionCode
        {
            public void RunCode()
            {
                var myInstance = Activator.CreateInstance<ReflectedClass>() as ReflectedClass;
                PropertyInfo[] myProperties = typeof(ReflectedClass).GetProperties();

                Console.WriteLine("Properties:");
                foreach (var prop in myProperties)
                    Console.WriteLine(prop.Name);
            }

            private class ReflectedClass
            {
                public int Id { get; set; }
                public string Name { get; set; }

                public ReflectedClass(int id, string name)
                {
                    this.Id = id;
                    this.Name = name;
                }

                public ReflectedClass()
                {
                }
            }
        }
        #endregion

        #region "Delegates"

        private class DelegateCode
        {
            public delegate void Del(string message);

            public void RunCode()
            {
                Del handler = DelegateMethod;
                handler("Hello Delegate");
            }

            public void DelegateMethod(string text)
            {
                Console.WriteLine(text);
            }
        }

        #endregion

        #region "Generics"

        private class GenericsCode
        {
            public void RunCode()
            {
                var code = new GenericsImpl<string>();
                code.Add("Hello Generics");
                var code2 = code.Add2<int>(123456);
            }


            private class GenericsImpl<T>
            {
                public List<T> ListProp { get; set; }

                public GenericsImpl()
                {
                    this.ListProp = new List<T>();
                }

                public void Add(T t)
                {
                    ListProp.Add(t);
                }

                public string Add2<T2>(T2 t)
                {
                    return t.ToString();
                }
            }
        }

        #endregion

        #region "Extension methods"
        private class ExtensionCode
        {
            public void RunCode()
            {
                var str = "String to extend.";
                Console.WriteLine(str.MyExtension());
                Console.WriteLine();

                int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                foreach (var item in array.Where2(x => x % 2 == 0))
                    Console.WriteLine("Number {0} is even", item);

                Console.WriteLine();

                foreach (var item in array.AsParallel().Where2(x => x % 2 != 0))
                    Console.WriteLine("Number {0} is odd", item);


                var range = Enumerable.Range(0, 10000000);
                var stopwatch = new Stopwatch();
                var range1 = new List<int>();
                var range2 = new List<int>();

                stopwatch.Start();
                foreach (var item in array.AsParallel().Where2(x => Odd(x)))
                    range1.Add(item);
                stopwatch.Stop();
                Console.WriteLine("PLINQ {0} ms", stopwatch.ElapsedMilliseconds);

                stopwatch.Restart();
                foreach (var item in array.Where2(x => Odd(x)))
                    range2.Add(item);
                stopwatch.Stop();
                Console.WriteLine("LINQ {0} ms", stopwatch.ElapsedMilliseconds);
            }

            private bool Odd(int x)
            {
                Thread.Sleep(100);
                return x % 2 != 0;
            }
        }
        #endregion
    }

    #region "Extension Methods"    
    public static class MyExtensions
    {
        public static string MyExtension(this string str)
        {
            return "Extended: " + str;
        }

        public static IEnumerable<T> Where2<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }


    }
    #endregion
}
