using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCrawlerProxyList.Data;
using WebCrawlerProxyList.Interfaces;
using WebCrawlerProxyList.Repositories;
using WebCrawlerProxyList.Services;

namespace WebCrawlerProxyList
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var basePath = AppContext.BaseDirectory;
            var appSettingsPath = Path.Combine(basePath, @"..\..\..\appsettings.json");

            appSettingsPath = Path.GetFullPath(appSettingsPath);

            // Verifica se o arquivo appsettings.json existe.
            if (!File.Exists(appSettingsPath))
            {
                MessageBox.Show("Arquivo appsettings.json não encontrado!");
                return;
            }

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var serviceProvider = new ServiceCollection()
                .AddDbContext<CrawlerContext>(options =>
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)))
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<ICrawlerRepository, CrawlerRepository>()
                .AddSingleton<WebCrawlerService>()
                .AddHttpClient()
                .AddSingleton<MainForm>()
                .BuildServiceProvider();


            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }
    }
}