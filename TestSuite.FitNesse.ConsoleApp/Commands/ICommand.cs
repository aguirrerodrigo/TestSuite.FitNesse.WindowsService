namespace TestSuite.FitNesse.ConsoleApp.Commands
{
    internal interface ICommand
    {
        string Information { get; }
        void Execute(params string[] parameters);
    }
}