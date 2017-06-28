using System.ServiceProcess;

namespace TestSuite.FitNesse.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FitNesse()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
