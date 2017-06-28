using System;

namespace TestSuite.FitNesse
{
    public class FitNesseException : Exception
    {
        public FitNesseException() { }

        public FitNesseException(string message) : base(message) { }

        public FitNesseException(string message, Exception innerException) : base(message, innerException) { }
    }
}