using System;
using System.Xml.Serialization;
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
                    services.AddHostedService<App>();
                });


        class Game :IGame
        {
            public IGameSettings _settings { get; }
            public IGamePlatform _platform { get; }
            public ISettingsProvider _settingsProvider { get; }

            public int _userTryCount { get; set; }
            public int _userAnswer{ get; set; }

            public Game(IGameSettings settings,IGamePlatform platform,ISettingsProvider settingsProvider)
            {
                _settings = settings;
                _platform = platform;
                _settingsProvider = settingsProvider;

                _userTryCount = 0;

                SetSettings();
            }

            public void SetSettings()
            {
                _settings.MaxValue = _settingsProvider.GetMaxValue();
                _settings.MinValue = _settingsProvider.GetMinValue();
                _settings.TryCount = _settingsProvider.GetTryCount();

            }
            public int Start()
            {
                _settings.TagretValue = new Random().Next(_settings.MinValue, _settings.MaxValue);

                do
                {
                    _userAnswer = _platform.GetUserAnswer();
                    if (_userAnswer == _settings.TagretValue)
                    {
                        _platform.DisplayMessage($"Победа!!! Вы угадали число!!! Вам потребовалось {_userTryCount} попытки!!!");
                        return 1;
                    }
                    else if (_userAnswer > _settings.TagretValue)
                        _platform.DisplayMessage("Загаданное число меньше");
                    else if (_userAnswer < _settings.TagretValue)
                        _platform.DisplayMessage("Загаданное число больше");

                    _userTryCount++;
                }
                while (_settings.TryCount >= _userTryCount);

                _platform.DisplayMessage($"Вы не угадали число {_settings.TagretValue} за {_settings.TryCount} попыток... Попробуйте еще раз!");

                return 0;
            }

        }


        class SettingsProviderFromConsole : ISettingsProvider
        {
            public int GetMaxValue()
            {
                string svalue = "";
                int value;
                while (!int.TryParse(svalue,out value))
                {
                    Console.WriteLine("Введите верхнюю границу для рандомизатора");
                    svalue = Console.ReadLine();
                }
                return value;
            }

            public int GetMinValue()
            {
                string svalue = "";
                int value;
                while (!int.TryParse(svalue, out value))
                {
                    Console.WriteLine("Введите нижнюю границу для рандомизатора");
                    svalue = Console.ReadLine();
                }
                return value;
            }

            public int GetTryCount()
            {
                string svalue = "";
                int value;
                while (!int.TryParse(svalue, out value))
                {
                    Console.WriteLine("Введите число попыток");
                    svalue = Console.ReadLine();
                }
                return value;
            }
        }

        class GamePlatformConlose : IGamePlatform
        {
            public void DisplayMessage(string message)
            {
                Console.WriteLine(message);
            }

            public int GetUserAnswer()
            {
                Console.Write("Попробуйте унадать число: ");
                string svalue = "";
                int value;
                while (!int.TryParse(svalue, out value))
                    svalue = Console.ReadLine();
                return value;
            }
        }

        class GameSettings : IGameSettings
        {
            public int TagretValue { get; set; }
            public int MaxValue { get; set; }
            public int MinValue { get; set; }
            public int TryCount { get; set; }
        }
    }
}