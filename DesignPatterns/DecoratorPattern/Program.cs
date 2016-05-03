using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Coffee myZoegas = new Zoegas();
            Console.WriteLine(myZoegas.GetDescription() + ": " + myZoegas.GetCost() + " kr");

            Coffee myGevalia = new Gevalia();
            myGevalia = new MilkDecorator(myGevalia);
            myGevalia = new SugerDecorator(myGevalia);
            Console.WriteLine(myGevalia.GetDescription() + ": " + myGevalia.GetCost() + " kr");

            Console.Write("\nPress any key to quit!");
            Console.ReadKey();
        }
    }

    abstract class Coffee
    {
        protected string _description = "Unknown coffee";

        public virtual string GetDescription()
        {
            return _description;
        }

        public abstract decimal GetCost();
    }

    class Gevalia : Coffee
    {
        public Gevalia()
        {
            _description = "Gevalia";
        }

        public override decimal GetCost()
        {
            return 20.5M;
        }
    }

    class Zoegas : Coffee
    {
        public Zoegas()
        {
            _description = "Zoegas";
        }
            
        public override decimal GetCost()
        {
            return 25M;
        }
    }

    class MilkDecorator : Coffee
    {
        private Coffee _coffee;

        public MilkDecorator(Coffee coffee)
        {
            _coffee = coffee;
        }

        public override decimal GetCost()
        {
            return _coffee.GetCost() + 5M;
        }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", Milk";
        }
    }

    class SugerDecorator : Coffee
    {
        private Coffee _coffee;

        public SugerDecorator(Coffee coffee)
        {
            _coffee = coffee;
        }

        public override decimal GetCost()
        {
            return _coffee.GetCost() + 2M;
        }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", Sugar";
        }
    }

    
}
