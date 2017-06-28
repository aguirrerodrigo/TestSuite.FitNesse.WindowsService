namespace TestSuite.FitNesse.ConsoleApp.Commands
{
    internal class ExitCommand : ICommand
    {
        private IConsoleApplication application;

        public string Information { get; private set; }
            = "Exits the console application.";

        public ExitCommand(IConsoleApplication application)
        {
            this.application = application;
        }

        public void Execute(params string[] parameters)
        {
            this.application.IsExiting = true;
        }
    }
}