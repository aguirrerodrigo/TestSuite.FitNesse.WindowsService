using Microsoft.Extensions.Configuration;

namespace TestSuite.FitNesse
{
    internal class AppSettings
    {
        private IConfiguration configuration;

        public static AppSettings Instance { get; private set; } = new AppSettings();

        public string Port
        {
            get { return this.configuration["port"]; }
        }

        private AppSettings()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("fitnesse.json", optional: false);

            this.configuration = builder.Build();
        }
    }
}
