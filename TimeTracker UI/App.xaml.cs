using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;
using TimeTracker.UI.Core.Extensions;
using TimeTracker.UI.Models;
using TimeTracker.UI.Views;

namespace TimeTracker.UI;

public partial class App : Application
{
    public static IHost Host { get; private set; } = null!;

    public static TrackedTask? CurrentTask { get; set; } = null;

    private void Application_Startup (object sender, StartupEventArgs e)
    {
        Host = ConfigureHosting();
        
        var navigation = Host.Services.GetService<Core.Services.NavigationService>()!;
        navigation.SetMainWindow<MainWindow>();
    }

    private static IHost ConfigureHosting ()
    {
        var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory() + "/../../../Resources/")
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var config = builder.Build();

        var hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder().ConfigureServices(services => {
            services.AddSingleton(typeof(IConfiguration), config);
            services.AddServices();
            services.AddViewModels();
            services.AddViews();
        });

        return hostBuilder.Build();
    }
}