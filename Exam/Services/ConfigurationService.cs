using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace Exam.Services
{
    public static class ConfigurationService
    {
        public static IConfigurationRoot Configuration { get; private set; }

        public static void Init()
        {
            DbProviderFactories.RegisterFactory("SqlProvider", SqlClientFactory.Instance);

            if (Configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                Configuration = configurationBuilder.AddJsonFile("appSettings.json").Build();
            }
        }
    }
}

