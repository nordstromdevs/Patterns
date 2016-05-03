using System;
using System.Configuration;
using System.Threading.Tasks;
using static System.Math;

namespace C_6._0
{
    // Design philosophy:
    // -no big new concepts
    // -many small features
    // -clean up you code
        
    class Point
    {
        public event EventHandler OnChanged;

        // Getter-only auto-properties        
        public int X { get; }
        public int Y { get; }

        // Expression-bodied properties
        public double Dist => Sqrt(X * X + Y * Y);

        public Point(int x, int y)
        {
            X = x;
            Y = y;

            // using static members (see "using static System.Math;" above!)
            var dist = Sqrt(X * X + Y * Y);
        }

        public override string ToString()
        {
            // String interpolation
            return $"({X}, {Y})"; 
        }

        public void Change()
        {
            // Null-conditional operators (Elvis operator)
            OnChanged?.Invoke(this, new EventArgs());
        }

        public async void Add(Point point)
        {
            // the nameof operator
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            try
            {

            } 
            catch (AppException e) when (e.IsFatal) // Exception filters
            {
                // await in catch and finally
                await LogAsync(e.Message);
            }
            finally
            {
                await LogAsync("");
            }
        }

        private async Task LogAsync(string message)
        {
            await Task.Delay(10);
            Console.WriteLine(message);
        }
    }

    class AppException : Exception
    {
        public bool IsFatal { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Point(10, 10);
            Console.WriteLine($"p1 = {p1.ToString()}");            

            Console.WriteLine("\nPress any key to quit!");
            Console.ReadKey();
        }
    }
}
