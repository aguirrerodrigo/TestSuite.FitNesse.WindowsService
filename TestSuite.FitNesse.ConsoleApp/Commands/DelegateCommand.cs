using System;

namespace TestSuite.FitNesse.ConsoleApp.Commands
{
    internal class DelegateCommand : ICommand
    {
        private Action action;

        public string Information { get; private set; }

        public DelegateCommand(string information, Action action)
        {
            this.Information = information;
            this.action = action;
        }

        public void Execute(params string[] parameters)
        {
            this.action();
        }
    }
}
