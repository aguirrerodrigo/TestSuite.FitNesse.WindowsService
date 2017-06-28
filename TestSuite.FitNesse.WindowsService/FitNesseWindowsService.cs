using System.ServiceProcess;

namespace TestSuite.FitNesse.WindowsService
{
    public partial class FitNesseWindowsService : ServiceBase
    {
        public FitNesseWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            FitNesse.Instance.Start();
        }

        protected override void OnStop()
        {
            FitNesse.Instance.Stop();
        }
    }
}
