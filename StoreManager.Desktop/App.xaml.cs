using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using StoreManager.Desktop.Views;
using StoreManager.Core.Interfaces;
using StoreManager.Core.Repositories;
using StoreManager.Core.Services;
using StoreManager.Core.ViewModels;

namespace StoreManager.Desktop
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public static IServiceProvider Services { get; private set; } = null!;
    protected override void OnStartup(StartupEventArgs e)
    {
      var host = Host.CreateDefaultBuilder()
                     .ConfigureServices(services =>
                      { 
                        services.AddSingleton<ILabelService, LabelService>();
                        services.AddSingleton<INodeRepository, SqliteNodeRepository>(); 
                        services.AddSingleton<ISearchService, SearchService>();
                        services.AddTransient<CanvasViewModel>(); 
                        services.AddTransient<BrowserViewModel>(); 
                        services.AddTransient<MainWindow>();
                      })
                     .Build();
      Services = host.Services;
      Services.GetRequiredService<MainWindow>().Show();
    }
  }
}
