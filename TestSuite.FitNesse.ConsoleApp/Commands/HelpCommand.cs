using System.Collections.Generic;
using System.IO;

namespace TestSuite.FitNesse.ConsoleApp.Commands
{
    internal class HelpCommand : ICommand
    {
        private Dictionary<string, ICommand> commands;
        private TextWriter output;

        public string Information { get; private set; }
            = "Prints help information.";
        
        public HelpCommand(Dictionary<string, ICommand> commands, TextWriter output)
        {
            this.commands = commands;
            this.output = output;
        }

        public void Execute(params string[] parameters)
        {
            this.output.WriteLine("List of commands:");
            foreach (var kvp in this.commands)
            {
                this.output.WriteLine($"\t{kvp.Key}\t{kvp.Value.Information}");
            }
        }
    }
}