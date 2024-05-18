using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;
using TimeTracker.UI.Core.Extensions;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI;

public partial class App : Application
{
    public static IHost Container { get; private set; } = null!;

    private void Application_Startup (object sender, StartupEventArgs e)
    {
        Container = ConfigureHosting();

        var mainView = Container.Services.GetRequiredService<IView<NavigationViewModel>>();
        mainView.Display();
    }

    /// <summary>
    /// Выполняет action в главном потоке WPF
    /// 
    /// Методы, работающие с UI приходится вызывать через Dispatcher,
    /// т.к. WPF очень не любит, когда с ним работают из разных потоков
    /// </summary>
    public static void ExecuteSync (Action action)
    {
        Current.Dispatcher.Invoke(action);
    }

    private static IHost ConfigureHosting ()
    {
        var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory() + "/../../../Resources/")
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var config = builder.Build();

        var hostBuilder = Host.CreateDefaultBuilder().ConfigureServices(services => {
            services.AddSingleton(typeof(IConfiguration), config);
            services.AddServices();
            services.AddViewModels();
            services.AddViews();
        });

        return hostBuilder.Build();
    }
}