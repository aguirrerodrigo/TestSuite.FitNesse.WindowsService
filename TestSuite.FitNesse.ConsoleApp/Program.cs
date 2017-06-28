using System;
using TestSuite.FitNesse.ConsoleApp.Commands;

namespace TestSuite.FitNesse.ConsoleApp
{
    class Program
    {
        static Program()
        {
            FitNesse.Instance.AbruptlyStopped += FitNesse_AbruptlyStopped;
        }

        private static void FitNesse_AbruptlyStopped(object sender, EventArgs e)
        {
            Console.WriteLine("\r\nFitNesse process abruptly stopped. Please check log file for details.");
        }

        static void Main(string[] args)
        {
            var commandManager = new CommandManager(Console.Out);

            // Add commands
            commandManager.Add("start",
                new DelegateCommand("Starts FitNesse process.", StartProcess));
            commandManager.Add("stop",
                new DelegateCommand("Stops FitNesse process.", StopProcess));
            commandManager.Add("is-running",
                new DelegateCommand("Checks if FitNesse process is running.", IsProcessRunning));

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

        private static void IsProcessRunning()
        {
            if (FitNesse.Instance.IsRunning)
                Console.WriteLine("FitNesse process running.");
            else
                Console.WriteLine("FitNesse process stopped.");
        }
    }
}
