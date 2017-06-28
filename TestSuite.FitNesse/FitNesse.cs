using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace TestSuite.FitNesse
{
    public class FitNesse
    {
        public event EventHandler AbruptlyStopped;

        private LogWriter logger;
        private Process fitnesseProc;

        public static FitNesse Instance { get; private set; } = new FitNesse();

        public bool IsRunning
        {
            get { return fitnesseProc != null && !fitnesseProc.HasExited; }
        }

        private FitNesse()
        {
            var logWriterFactory = new LogWriterFactory();
            logger = logWriterFactory.Create();
        }

        public void Start()
        {
            if (this.IsRunning)
                throw new FitNesseException("Could not start new FitNesse process. A FitNesse process is already running.");

            this.fitnesseProc = new Process();
            this.fitnesseProc.StartInfo = BuildStartInfo();

            this.HookEvents(this.fitnesseProc);

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
        private void HookEvents(Process fitnesseProc)
        {
            this.fitnesseProc.EnableRaisingEvents = true;
            this.fitnesseProc.OutputDataReceived += OnOutputDataReceived;
            this.fitnesseProc.ErrorDataReceived += OnErrorDataReceived;
            this.fitnesseProc.Exited += OnExited;
        }

        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
                logger.Write(e.Data);
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
                logger.Write(e.Data);
        }

        private void OnExited(object sender, EventArgs e)
        {
            this.UnhookEvents(sender as Process);
            this.AbruptlyStopped?.Invoke(this, new EventArgs());
        }

        public void Stop()
        {
            if (!this.IsRunning)
                throw new FitNesseException("Could not stop FitNesse process. FitNesse process not yet started.");

            this.UnhookEvents(this.fitnesseProc);
            this.fitnesseProc.Kill();
        }

        private void UnhookEvents(Process fitnesseProc)
        {
            this.fitnesseProc.OutputDataReceived -= OnOutputDataReceived;
            this.fitnesseProc.ErrorDataReceived -= OnErrorDataReceived;
            this.fitnesseProc.Exited -= OnExited;
        }
    }
}
