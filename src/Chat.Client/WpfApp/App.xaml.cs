using Core;
using Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfApp.Configuration;
using WpfApp.ViewModels;
using WpfApp.Views;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            var config = new WpfConfiguration();

            services.AddSingleton<IWpfConfiguration>(config);

            services.AddScoped<MainViewModel>();
            services.AddScoped<MainWindow>();

            services.AddCore(config);

            var provider = services.BuildServiceProvider();

            var mainWindow = provider.GetService<MainWindow>();

            System.Windows.Application.Current.MainWindow = mainWindow;
            mainWindow.InitializeComponent();
            mainWindow.Show();
        }
    }

}
