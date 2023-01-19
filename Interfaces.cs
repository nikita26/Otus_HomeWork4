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
        /// Верхняя граница для рандомизатора
        /// </summary>
        int MaxValue { get; set; }

        /// <summary>
        /// Нижняя граница для рандомизатора
        /// </summary>
        int MinValue { get; set; }

        /// <summary>
        /// Количество попыток отведенное для пользователя
        /// </summary>
        int TryCount { get; set; }
    }

    /// <summary>
    /// Поставщик настроек для игры
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Получить настройки для игры
        /// </summary>
        /// <returns>Экземпляр настроек игры</returns>
        IGameSettings GetGameSettings();
    }

    /// <summary>
    /// Платформа запуска игры
    /// </summary>
    public interface IGamePlatform
    {
        /// <summary>
        /// Получить число от пользователя
        /// </summary>
        /// <returns></returns>
        int GetUserAnswer();

        /// <summary>
        /// Отобразить сообщения для пользователя
        /// </summary>
        /// <param name="message"></param>
        void DisplayMessage(string message);
    }

    /// <summary>
    /// Игра
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Настройки игры
        /// </summary>
        IGameSettings _settings { get; }

        /// <summary>
        /// Платформа запуска игры
        /// </summary>
        IGamePlatform _platform { get; }

        /// <summary>
        /// Поставщик настроек игры
        /// </summary>
        ISettingsProvider _settingsProvider { get; }

        /// <summary>
        /// Количетсво сделанных пользователям попыток
        /// </summary>
        int _userTryCount { get; set; }

        /// <summary>
        /// Текущий ответ пользователя в текущую попытку
        /// </summary>
        int _userAnswer{ get; set; }


        /// <summary>
        /// Установить настройки для игры
        /// </summary>
        void SetSettings();

        /// <summary>
        /// Запуск игры
        /// </summary>
        /// <returns></returns>
        int Start();
    }
}
