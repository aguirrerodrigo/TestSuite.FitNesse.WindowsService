using System.ServiceProcess;

namespace TestSuite.FitNesse.WindowsService
{
    public partial class FitNesse : ServiceBase
    {
        public FitNesse()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
