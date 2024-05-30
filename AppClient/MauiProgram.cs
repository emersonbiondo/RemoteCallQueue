using Microsoft.Extensions.Logging;
using AppClient.Presentations.ViewModels;
using AppClient.Presentations.Views;
using CommunityToolkit.Maui;
using AppServer;
using Microsoft.Extensions.Configuration;
using AppClient.Util;

namespace AppClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT_DEVELOPMENT");

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();

            var clientConfiguration = configuration.GetSection(nameof(ClientConfiguration)).Get<ClientConfiguration>();

            builder.Services.AddSingleton(clientConfiguration);

            builder.Services.AddSingleton<IAlertService, AlertService>();

            builder.Services.AddTransient<ClientViewModel>();

            builder.Services.AddTransient<ClientView>();

            return builder.Build();
        }
    }
}
