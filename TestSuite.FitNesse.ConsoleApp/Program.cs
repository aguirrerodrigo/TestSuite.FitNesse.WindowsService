using System;
using TestSuite.FitNesse.ConsoleApp.Commands;

namespace TestSuite.FitNesse.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandManager = new CommandManager(Console.Out);

            // Add commands
            commandManager.Add("start",
                new DelegateCommand("Starts FitNesse process.", StartProcess));
            commandManager.Add("stop",
                new DelegateCommand("Stops FitNesse process.", StopProcess));

            commandManager.Help();
            while (!commandManager.IsExiting)
            {
                try
                {
                    Console.Write("Enter command: ");
                    var command = Console.ReadLine();
                    commandManager.Execute(command);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void StartProcess()
        {
            FitNesse.Instance.Start();
        }

        private static void StopProcess()
        {
            FitNesse.Instance.Stop();
        }
    }
}