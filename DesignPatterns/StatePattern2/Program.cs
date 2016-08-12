using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern2
{
    class Program
    {
        static void Main(string[] args)
        {
            var myMachine = new Machine();
            myMachine.Run();

            Console.Write("\nPress any key to close!");
            Console.ReadKey();
        }
    }

    class Machine
    {
        private State _state;
        public WelcomeState WelcomeStateProp { get; }
        public InGameState InGameStateProp { get; }
        public GameOverState GameOverStateProp { get; }

        public Machine()
        {
            this.WelcomeStateProp = new WelcomeState(this);
            this.InGameStateProp = new InGameState(this);
            this.GameOverStateProp = new GameOverState(this);
        }

        public void Run()
        {
            SetState(this.WelcomeStateProp);
            _state.Jump(); // not valid
            _state.InsertCoin();
            _state.Jump();
            _state.Quit();
            _state.Jump(); // not valid
        }

        public void SetState(State state)
        {
            _state = state;
            _state.Display();
        }     
    }

    abstract class State
    {
        protected Machine _machine;        

        public State(Machine machine)
        {
            _machine = machine;            
        }

        public virtual void Display()
        {
            Console.WriteLine("You are in " + this.ToString());
        }

        public virtual void InsertCoin()
        {
            Console.WriteLine("InsertCoin() - Not a valid thing to do in this state!");
        }

        public virtual void Jump()
        {
            Console.WriteLine("Jump() - Not a valid thing to do in this state!");
        }

        public virtual void Quit()
        {
            Console.WriteLine("Quit() - Not a valid thing to do in this state!");
        }
    }

    class WelcomeState : State
    {
        public WelcomeState(Machine machine) : base(machine)
        { }

        public override void InsertCoin()
        {
            Console.WriteLine("WelcomeState.InsertCoin()");
            _machine.SetState(_machine.InGameStateProp);
        }
    }

    class InGameState : State
    {
        public InGameState(Machine machine) : base(machine)
        { }

        public override void Jump()
        {
            Console.WriteLine("InGameState.Jump()");
        }

        public override void Quit()
        {
            Console.WriteLine("InGameState.Quit()");
            _machine.SetState(_machine.GameOverStateProp);
        }

    }

    class GameOverState : State
    {
        public GameOverState(Machine machine) : base(machine)
        { }

        public override void Display()
        {
            base.Display();
            _machine.SetState(_machine.WelcomeStateProp);
        }
    }
}
