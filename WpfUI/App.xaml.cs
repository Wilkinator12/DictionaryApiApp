using DictionaryLibrary.DataAccess;
using DictionaryLibrary.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IConfiguration _configuration;
        private IServiceProvider _serviceProvider;


        public App() => Configure();


        [MemberNotNull(nameof(_configuration), nameof(_serviceProvider))]
        private void Configure()
        {
            _configuration = GetConfiguration();
            _serviceProvider = GetServiceProvider();
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
            services
                .AddHttpClient()
                .Configure<DictionaryApiOptions>(_configuration.GetSection(nameof(DictionaryApiOptions)))
                .AddTransient<IDictionaryApiDataAccess, DictionaryApiDataAccess>()
                .AddTransient<MainWindow>();

            ConfigureLogging(services);
        }

        private void ConfigureLogging(IServiceCollection services)
        {
            string? filePath = _configuration.GetValue<string>("LogFilePaths:Default");

            if (filePath == null) throw new NullReferenceException("Log file path not found.");


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

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow!.Show(); // POSSIBLE NULL REFERENCE SUPRESSION
        }
    }
}
