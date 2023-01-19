using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Otus_HomeWork4
{
    class Programm
    {
        public static void Main(string[] args)
        {
            CreateAppBuilder(args)
                .ConfigureServices(services => services
                .AddTransient<IGame, Game>()
                .AddTransient<IGamePlatform, GamePlatformConlose>()
                .AddTransient<IGameSettings, GameSettings>()
                .AddTransient<ISettingsProvider, SettingsProviderFromConsole>())

                .ConfigureLogging(s => s.ClearProviders())

                .Build()
                .Run();
        }

        public static IHostBuilder CreateAppBuilder(string[] args)=>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<StartUp>();
                });
    }
}