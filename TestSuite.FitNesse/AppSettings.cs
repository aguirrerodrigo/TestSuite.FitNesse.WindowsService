using System.Configuration;

namespace TestSuite.FitNesse
{
    internal class AppSettings
    {
        public static AppSettings Instance { get; private set; } = new AppSettings();

        public string Port
        {
            get { return this.RequireSetting("FitNesse.Port"); }
        }

        private AppSettings() { }

        private string RequireSetting(string key)
        {
            var result = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(result))
                throw new System.Configuration.SettingsPropertyNotFoundException($"Could not find AppSetting with key '{key}'.");

            return result;
        }
    }
}
