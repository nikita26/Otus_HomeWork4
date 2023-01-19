using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus_HomeWork4
{
    /// <summary>
    /// Игра
    /// </summary>
    public class Game : IGame
    {
        public IGameSettings _settings { get; private set; }
        public IGamePlatform _platform { get; }
        public ISettingsProvider _settingsProvider { get; }

        public int _userTryCount { get; set; }
        public int _userAnswer { get; set; }

        public Game(IGameSettings settings, IGamePlatform platform, ISettingsProvider settingsProvider)
        {
            _settings = settings;
            _platform = platform;
            _settingsProvider = settingsProvider;

            _userTryCount = 1;

            SetSettings();
        }

        public void SetSettings()
        {
            _settings = _settingsProvider.GetGameSettings();
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


    /// <summary>
    /// Поставщик настроек(из консоли, ручной ввод)
    /// </summary>
    public class SettingsProviderFromConsole : ISettingsProvider
    {
        public IGameSettings GetGameSettings()
        {
            string sMaxValue = "";
            int maxValue;
            while (!int.TryParse(sMaxValue, out maxValue))
            {
                Console.WriteLine("Введите верхнюю границу для рандомизатора");
                sMaxValue = Console.ReadLine();
            }

            string sMinValue = "";
            int minValue;
            while (!int.TryParse(sMinValue, out minValue))
            {
                Console.WriteLine("Введите нижнюю границу для рандомизатора");
                sMinValue = Console.ReadLine();
            }

            string sTryCount = "";
            int tryCoun;
            while (!int.TryParse(sTryCount, out tryCoun))
            {
                Console.WriteLine("Введите число попыток");
                sTryCount = Console.ReadLine();
            }

            return new GameSettings { 
                MaxValue = maxValue,
                MinValue = minValue,
                TryCount= tryCoun
            };
        }
    }

    /// <summary>
    /// Платформа запуска игры (консоль)
    /// </summary>
    public class GamePlatformConlose : IGamePlatform
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

    /// <summary>
    /// Настройки игры
    /// </summary>
    public class GameSettings : IGameSettings
    {
        public int TagretValue { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int TryCount { get; set; }
    }

}