using System;

namespace TestSuite.FitNesse.ConsoleApp.Commands
{
    internal class ConsoleApplicationException : Exception
    {
        public ConsoleApplicationException() { }
        public ConsoleApplicationException(string message) : base(message) { }
        public ConsoleApplicationException(string message, Exception inner) : base(message, inner) { }
    }
}
