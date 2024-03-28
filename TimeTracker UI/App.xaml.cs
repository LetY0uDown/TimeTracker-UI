using System.Windows;
using TimeTracker.UI.Views;

namespace TimeTracker.UI;

public partial class App : Application
{
    private void Application_Startup (object sender, StartupEventArgs e)
    {
        Current.MainWindow = new MainWindow ();
        Current.MainWindow.Show ();
    }
}