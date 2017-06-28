using System;
using System.Runtime.Serialization;

namespace TestSuite.FitNesse.WindowsService
{
    [Serializable]
    public class WindowsServiceException : Exception
    {
        public WindowsServiceException() { }
        public WindowsServiceException(string message) : base(message) { }
        public WindowsServiceException(string message, Exception inner) : base(message, inner) { }
        protected WindowsServiceException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
