using System.ServiceProcess;

namespace TestSuite.FitNesse.WindowsService
{
    public partial class FitNesseWindowsService : ServiceBase
    {
        public FitNesseWindowsService()
        {
            InitializeComponent();

            FitNesse.Instance.AbruptlyStopped += FitNesse_AbruptlyStopped;
        }

        private void FitNesse_AbruptlyStopped(object sender, System.EventArgs e)
        {
            throw new WindowsServiceException("FitNesse process abruptly stopped. Please check log file for details.");
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