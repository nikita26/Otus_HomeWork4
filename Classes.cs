using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Otus_HomeWork4
{
    public class Game : IGame
    {
        public IGameSettings _settings { get; }
        public IGamePlatform _platform { get; }
        public ISettingsProvider _settingsProvider { get; }
        public ICollection<AnswerUser> _answersUser { get; set; }

        public int _userTryCount { get; set; }
        public int _userAnswer { get; set; }

        public Game(IGameSettings settings, IGamePlatform platform, ISettingsProvider settingsProvider)
        {
            _settings = settings;
            _platform = platform;
            _settingsProvider = settingsProvider;
            _answersUser = new List<AnswerUser>();

            _userTryCount = 1;

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
                    _answersUser.Add(new CorrectAnswerUser{ Value = _userAnswer,Index = _userTryCount });

                    _platform.DisplayMessage($"Победа!!! Вы угадали число!!! Вам потребовалось {_userTryCount} попытки!!!");
                    ShowStatistics(_platform);
                    return 1;
                }
                else if (_userAnswer > _settings.TagretValue)
                    _platform.DisplayMessage("Загаданное число меньше");
                else if (_userAnswer < _settings.TagretValue)
                    _platform.DisplayMessage("Загаданное число больше");

                _answersUser.Add(new AnswerUser{ Value = _userAnswer, Index = _userTryCount });

                _userTryCount++;
            }
            while (_settings.TryCount >= _userTryCount);

            _platform.DisplayMessage($"Вы не угадали число {_settings.TagretValue} за {_settings.TryCount} попыток... Попробуйте еще раз!");

            return 0;
        }
        public void ShowStatistics(IGamePlatform platform)
        {
            foreach(var answer in _answersUser)
                platform.DisplayMessage(answer.GetAnswer());
        }
    }


    /// <summary>
    /// Класс получающий конфигурацию
    /// S - принцип единой ответственности
    /// </summary>
    public class SettingsProviderFromConsole : ISettingsProvider
    {
        public int GetMaxValue()
        {
            string svalue = "";
            int value;
            while (!int.TryParse(svalue, out value))
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

    /// <summary>
    /// Класс реализующий платформу выполнения игры
    /// S - принцип единой ответственности
    /// </summary>
    public class GamePlatformConlose : IGamePlatform
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public int GetUserAnswer()
        {
            Console.Write("Попробуйте угадать число: ");
            string svalue = "";
            int value;
            while (!int.TryParse(svalue, out value))
                svalue = Console.ReadLine();
            return value;
        }
    }

    /// <summary>
    /// Класс для храннеия настроек игры
    /// S - принцип единой ответственности
    /// </summary>
    public class GameSettings : IGameSettings
    {
        public int TagretValue { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int TryCount { get; set; }
    }

    /// <summary>
    /// Класс ответа пользователя
    /// L - принцип подстановки Лисков
    /// </summary>
    public class AnswerUser
    {
        public int Value { get; init; }

        public int Index { get; init; }

        virtual public string GetAnswer() =>
            $"Попытка #{Index} : {Value}";
    }
    /// <summary>
    /// Класс верного ответа пользователя
    /// L - принцип подстановки Лисков
    /// </summary>
    public class CorrectAnswerUser : AnswerUser
    {
        public override string GetAnswer() =>
            $"Успешная попытка #{Index} : {Value}!";
    }
}