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
        public IGameSettings Settings { get; }
        public IGamePlatform Platform { get; }
        public ISettingsProvider SettingsProvider { get; }
        public ICollection<AnswerUser> AnswersUser { get; set; }

        public int UserTryCount { get; set; }
        public int UserAnswer { get; set; }

        public Game(IGameSettings settings, IGamePlatform platform, ISettingsProvider settingsProvider)
        {
            Settings = settings;
            Platform = platform;
            SettingsProvider = settingsProvider;
            AnswersUser = new List<AnswerUser>();

            UserTryCount = 1;

            SetSettings();
        }

        public void SetSettings()
        {
            Settings.MaxValue = SettingsProvider.GetMaxValue();
            Settings.MinValue = SettingsProvider.GetMinValue();
            Settings.TryCount = SettingsProvider.GetTryCount();

        }
        public int Start()
        {
            Settings.TagretValue = new Random().Next(Settings.MinValue, Settings.MaxValue);

            do
            {
                UserAnswer = Platform.GetUserAnswer();
                if (UserAnswer == Settings.TagretValue)
                {
                    AnswersUser.Add(new CorrectAnswerUser{ Value = UserAnswer,Index = UserTryCount });

                    Platform.DisplayMessage($"Победа!!! Вы угадали число!!! Вам потребовалось {UserTryCount} попытки!!!");
                    ShowStatistics(Platform);
                    return 1;
                }
                else if (UserAnswer > Settings.TagretValue)
                    Platform.DisplayMessage("Загаданное число меньше");
                else if (UserAnswer < Settings.TagretValue)
                    Platform.DisplayMessage("Загаданное число больше");

                AnswersUser.Add(new AnswerUser{ Value = UserAnswer, Index = UserTryCount });

                UserTryCount++;
            }
            while (Settings.TryCount >= UserTryCount);

            Platform.DisplayMessage($"Вы не угадали число {Settings.TagretValue} за {Settings.TryCount} попыток... Попробуйте еще раз!");

            return 0;
        }
        public void ShowStatistics(IGamePlatform platform)
        {
            foreach(var answer in AnswersUser)
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