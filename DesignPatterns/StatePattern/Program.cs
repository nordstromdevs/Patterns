using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var myStateMachine = new StateMachine();
            myStateMachine.Run();

            Console.WriteLine("\nPress any key to quit!");
            Console.ReadKey();
        }
    }

    class StateMachine
    {
        private IState _state;
        private WelcomeState _welcomeState;
        private InGameState _inGameState;
        private GameOverState _gameOverState;

        public StateMachine()
        {
            _welcomeState = new WelcomeState(this);
            _inGameState = new InGameState(this);
            _gameOverState = new GameOverState(this);
        }

        public void Run()
        {
            SetState(_welcomeState);
            _state.Jump(); // not valid      
                 
            _state.InsertCoin();
            _state.Jump(); 
            _state.QuitGame();

            _state.QuitGame(); // not valid
        }

        public void SetState(IState state)
        {
            _state = state;
            _state.Display();
        }

        public WelcomeState GetWelcomeState()
        {
            return _welcomeState;
        }

        public InGameState GetInGameState()
        {
            return _inGameState;
        }

        public GameOverState GetGameOverState()
        {
            return _gameOverState;
        }

    }

    interface IState
    {
        void InsertCoin();
        void Jump(); 
        void QuitGame();
        void Display();
    }

    class WelcomeState : IState
    {
        private StateMachine _machine;
         
        public WelcomeState(StateMachine machine)
        {
            _machine = machine;
        }

        public void InsertCoin()
        {
            Console.WriteLine("WelcomeState: InsertCoin()");
            _machine.SetState(_machine.GetInGameState());
        }

        public void Display()
        {
            Console.WriteLine("You are in WelcomeState!");
        }

        public void Jump()
        {
            Console.WriteLine("Not a valid thing to do in this state!");
        }

        public void QuitGame()
        {
            Console.WriteLine("Not a valid thing to do in this state!");
        }     
    }

    class InGameState : IState
    {
        private StateMachine _machine;

        public InGameState(StateMachine machine)
        {
            _machine = machine;
        }

        public void Display()
        {
            Console.WriteLine("You are in InGameState!");
        }

        public void InsertCoin()
        {
            Console.WriteLine("Not a valid thing to do in this state!");
        }

        public void Jump()
        {
            Console.WriteLine("InGameState: Jump()");
        }

        public void QuitGame()
        {
            Console.WriteLine("InGameState: QuitGame()");
            _machine.SetState(_machine.GetGameOverState());
        }     
    }

    class GameOverState : IState
    {
        private StateMachine _machine;

        public GameOverState(StateMachine machine)
        {
            _machine = machine;
        }

        public void Display()
        {
            Console.WriteLine("You are in GameOverState!");
            _machine.SetState(_machine.GetWelcomeState());
        }

        public void InsertCoin()
        {
            Console.WriteLine("Not a valid thing to do in this state!");
        }

        public void Jump()
        {
            Console.WriteLine("Not a valid thing to do in this state!");
        }

        public void QuitGame()
        {
            Console.WriteLine("Not a valid thing to do in this state!");
        }      
    }
}
