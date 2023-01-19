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
        int TagretValue { get; set; }
        int MaxValue { get; set; }
        int MinValue { get; set; }
        int TryCount { get; set; }
    }

    /// <summary>
    /// Поставщик настроек для игры
    /// </summary>
    public interface ISettingsProvider
    {
        int GetMaxValue();
        int GetMinValue();
        int GetTryCount();
    }

    /// <summary>
    /// Платформа запуска игры
    /// </summary>
    public interface IGamePlatform
    {
        int GetUserAnswer();
        void DisplayMessage(string message);
    }

    /// <summary>
    /// Игра
    /// </summary>
    public interface IGame
    {
        IGameSettings _settings { get; }
        IGamePlatform _platform { get; }
        ISettingsProvider _settingsProvider { get; }

        int _userTryCount { get; set; }
        int _userAnswer{ get; set; }

        void SetSettings();
        int Start();
    }
}
