using System.Configuration;
using System.Data;
using System.Windows;

namespace AksharaShift;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        // Initialize and show the main window (which will be hidden in background)
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }
}

