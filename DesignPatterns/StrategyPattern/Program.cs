using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var smartPlayer = new SmartPlayer();
            var notSoSmartPlayer = new NotSoSmartPlayer();

            smartPlayer.PerformCompute();
            notSoSmartPlayer.PerformCompute();

            notSoSmartPlayer.ComputeBehavior = new SmartBehavior();
            notSoSmartPlayer.PerformCompute();

            Console.WriteLine("Press any key to close!");
            Console.ReadKey();
        }
    }

    abstract class Player
    {
        public IComputeBehavior ComputeBehavior { get; set; }

        public abstract void Display();

        public void PerformCompute()
        {
            ComputeBehavior.Compute();
        }
    }

    class SmartPlayer: Player
    {
        public SmartPlayer()
        {
            this.ComputeBehavior = new SmartBehavior();
        }

        public override void Display()
        {
            Console.WriteLine("I'm a smart player");
        }
    }

    class NotSoSmartPlayer : Player
    {
        public NotSoSmartPlayer()
        {
            this.ComputeBehavior = new DumbBehavior();
        }

        public override void Display()
        {
            Console.WriteLine("I'm a not so smart player");
        }
    }

    interface IComputeBehavior
    {
        void Compute();
    }

    class SmartBehavior : IComputeBehavior
    {
        public void Compute()
        {
            Console.WriteLine("I'm computing smart!");
        }
    }

    class DumbBehavior : IComputeBehavior
    {
        public void Compute()
        {
            Console.WriteLine("I'm computing quite dumb!");
        }
    }
}
