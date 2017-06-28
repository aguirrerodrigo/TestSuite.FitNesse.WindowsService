using System;
using System.Diagnostics;
using System.IO;

namespace TestSuite.FitNesse
{
    public class FitNesse
    {
        private Process fitnesseProc;

        public static FitNesse Instance { get; private set; } = new FitNesse();

        public bool IsRunning
        {
            get { return fitnesseProc != null && !fitnesseProc.HasExited; }
        }

        private FitNesse() { }

        public void Start()
        {
            if (this.fitnesseProc != null)
                throw new FitNesseException("Could not start new FitNesse process. A FitNesse process is already running.");

            this.fitnesseProc = new Process();
            this.fitnesseProc.StartInfo = BuildStartInfo();
            this.fitnesseProc.OutputDataReceived += OnOutputDataReceived;
            this.fitnesseProc.ErrorDataReceived += OnErrorDataReceived;
            this.fitnesseProc.Start();
            this.fitnesseProc.BeginOutputReadLine();
            this.fitnesseProc.BeginErrorReadLine();
        }        

        private ProcessStartInfo BuildStartInfo()
        {
            var result = new ProcessStartInfo();
            result.WorkingDirectory = Path.Combine(AppContext.BaseDirectory, "FitNesse");
            result.FileName = "java.exe";
            result.Arguments = $"-jar fitnesse-standalone.jar -p {AppSettings.Instance.Port}";
            result.CreateNoWindow = true;
            result.RedirectStandardOutput = true;
            result.RedirectStandardError = true;
            result.UseShellExecute = false;

            return result;
        }

        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("[ERROR]: " + e.Data);
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("[OUTPUT]: " + e.Data);
        }

        public void Stop()
        {
            if (this.fitnesseProc == null)
                throw new FitNesseException("Could not stop FitNesse process. FitNesse process not yet started.");

            this.fitnesseProc.Kill();
            this.fitnesseProc = null;
        }
    }
}
