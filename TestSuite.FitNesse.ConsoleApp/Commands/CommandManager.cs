using System;
using System.IO;
using System.Collections.Generic;

namespace TestSuite.FitNesse.ConsoleApp.Commands
{
    internal class CommandManager : IConsoleApplication
    {
        private Dictionary<string, ICommand> commands;

        public bool IsExiting { get; set; }

        public CommandManager(TextWriter output)
        {
            this.commands = new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase);

            var helpCommand = new HelpCommand(this.commands, output);
            this.commands.Add("help", helpCommand);

            var exitCommand = new ExitCommand(this);
            this.commands.Add("exit", exitCommand);
        }

        public bool Contains(string command)
        {
            return this.commands.ContainsKey(command);
        }

        public void Add(string name, ICommand command)
        {
            if (this.commands.ContainsKey(name))
                throw new ConsoleApplicationException($"Cannot add command. Command '{name}' aready exists.");

            this.commands.Add(name, command);
        }

        public bool Remove(string name)
        {
            return this.commands.Remove(name);
        }

        public void Help()
        {
            this.commands["help"].Execute();
        }

        public void Execute(string command, params string[] parameters)
        {
            if (!this.commands.ContainsKey(command))
                throw new ConsoleApplicationException($"Cannot execute command. Command '{command}' does not exist.");

            this.commands[command].Execute(parameters);
        }
    }
}