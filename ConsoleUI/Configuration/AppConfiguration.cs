using DictionaryLibrary.DataAccess;
using DictionaryLibrary.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleUI.Configuration
{
    public sealed class AppConfiguration
    {
        private static readonly Lazy<AppConfiguration> _instance = new Lazy<AppConfiguration>(() => new AppConfiguration());
        public static AppConfiguration Instance = _instance.Value;


        public IConfiguration Configuration { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }


        private AppConfiguration() => Configure();



        [MemberNotNull(nameof(Configuration), nameof(ServiceProvider))]
        private void Configure()
        {
            Configuration = GetConfiguration();
            ServiceProvider = GetServiceProvider();
        }


        private IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }


        private IServiceProvider GetServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            ConfigureServices(services);

            return services.BuildServiceProvider();
        }


        private void ConfigureServices(IServiceCollection services)
        {
            ConfigureLogging(services);
            services.AddHttpClient();
            services.Configure<DictionaryApiOptions>(Configuration.GetSection(nameof(DictionaryApiOptions)));
            services.AddTransient<IDictionaryApiDataAccess, DictionaryApiDataAccess>();
        }


        private void ConfigureLogging(IServiceCollection services)
        {
            string? filePath = Configuration.GetValue<string>("LogFilePaths:Default");

            if (filePath == null) throw new Exception($"Log file path not found");


            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(filePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddSingleton<ILogger>(logger);

            services.AddLogging(builder =>
            {
                builder.AddSerilog(logger);
            });
        }
    }
}
