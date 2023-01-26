using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Otus_HomeWork4
{
    class Programm
    {
        public static void Main(string[] args)
        {
            var builder = CreateAppBuilder(args);
            var app = ConfigurateApp(builder);
            app.Run();
        }

        /// <summary>
        /// Создание экземпляра приложения
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateAppBuilder(string[] args)=>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<App>();
                });

        /// <summary>
        /// Конфигурирвание приложения
        /// D - принцип инверсии зависимости
        /// </summary>
        public static IHost ConfigurateApp(IHostBuilder builder) =>
            builder.ConfigureServices(services => services
                .AddTransient<IGame, Game>()
                .AddTransient<IGamePlatform, GamePlatformConlose>()
                .AddTransient<IGameSettings, GameSettings>()
                .AddTransient<ISettingsProvider, SettingsProviderFromConsole>())
                .ConfigureLogging(s => s.ClearProviders())
                .Build();
    }
}