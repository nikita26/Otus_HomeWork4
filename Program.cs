using System;
using System.Xml.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Otus_HomeWork4
{
    class Programm
    {
        /// <summary>
        /// D - принцип инверсии зависимости
        /// </summary>
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
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
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