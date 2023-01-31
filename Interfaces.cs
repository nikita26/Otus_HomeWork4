using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus_HomeWork4
{
    /// <summary>
    /// Первоначальные настройки игры
    /// </summary>
    public interface IGameSettings
    {
        /// <summary>
        /// Загаданное число
        /// </summary>
        int TagretValue { get; set; }

        /// <summary>
        /// Верхняя граница рандомизатора
        /// </summary>
        int MaxValue { get; set; }

        /// <summary>
        /// Нижняя граница рандомизатора
        /// </summary>
        int MinValue { get; set; }

        /// <summary>
        /// Количество доступных попыток
        /// </summary>
        int TryCount { get; set; }
    }

    /// <summary>
    /// Поставщик настроек для игры
    /// O - принцип открытости для расширения и закрытости модификации
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Полувение верхней границы рандомизатора
        /// </summary>
        int GetMaxValue();
        /// <summary>
        /// Получение нижней границы рандомизатора
        /// </summary>
        int GetMinValue();
        /// <summary>
        /// Получение количества отведенных попыток для пользователя
        /// </summary>
        int GetTryCount();
    }

    /// <summary>
    /// Платформа запуска игры
    /// O - принцип открытости для расширения и закрытости модификации
    /// </summary>
    public interface IGamePlatform
    {
        /// <summary>
        /// Получеине ответа от пользователя 
        /// </summary>
        int GetUserAnswer();

        /// <summary>
        /// Вывод сообщения для пользователя
        /// </summary>
        void DisplayMessage(string message);
    }

    /// <summary>
    /// Абстракция экземпляра игры
    /// </summary>
    public interface IGame:IGameStarter,IGameConfigurator,IGameStatistics
    {
        /// <summary>
        /// Настройки игры
        /// </summary>
        IGameSettings Settings { get; }

        /// <summary>
        /// Платформа запуска игры
        /// </summary>
        IGamePlatform Platform { get; }

        /// <summary>
        /// Поставщик настроек игры
        /// </summary>
        ISettingsProvider SettingsProvider { get; }

        /// <summary>
        /// Количество совершенных пользователем попыток
        /// </summary>
        int UserTryCount { get; set; }

        /// <summary>
        /// Текущий ответ пользователя
        /// </summary>
        int UserAnswer{ get; set; }

    }

    /// <summary>
    /// Запуск игры
    /// I - принцип разделения интерфейсов
    /// </summary>
    public interface IGameStarter
    {
        /// <summary>
        /// Запуск игры
        /// </summary>
        int Start();
    }

    /// <summary>
    /// Получение настроек игры
    /// I - принцип разделения интерфейсов
    /// </summary>
    public interface IGameConfigurator
    {
        /// <summary>
        /// Загрузить стартовые настройки для игры
        /// </summary>
        void SetSettings();
    }

    /// <summary>
    /// Игровая статистика
    /// O - принцип открытости для расширения и закрытости модификации
    /// I - принцип разделения интерфейсов
    /// </summary>
    public interface IGameStatistics
    {
        /// <summary>
        /// Всё ответы пользователя
        /// </summary>
        public ICollection<AnswerUser> AnswersUser { get; set; }

        /// <summary>
        /// Демонстрация статистики пользователя 
        /// </summary>
        public void ShowStatistics(IGamePlatform platform);
    }
}
