# Otus_HomeWork4

Демонстрация SOLID принципов

SRP
	SettingsProviderFromConsole - класс поставщика конфигурации из консоли, ответственен только за получение конфига из консоли.
	GamePlatformConlose - класс реализуюший взаимодействие с платформой(консоль) на которой запускается игра, ответственен за вывод сообщений для пользователя и получение информации от пользователя в процессе игры.
	GameSettings - класс ответственен за хранение настроект игры.

OCP
	ISettingsProvider - интерфейс поставщика конфигурации игры, в нашем случае из консоли с помощью ручного ввода, может быть расширен путем реализации получения настроек из файла конфигурации или из базы данных
	IGamePlatform - интерфейс взаимодействия с платформой на которой запускается экземпляр игры, в нашем случае консольное приложение, может быть расширен путем реализаций взаимодействия с приложениями с графическим интерфейсом (WPF,UWP) или же с браузерным клиентом в случае запуска игры как веб приложения.
	IGameStatistics - интерфейс для демонстрации статистики, в нашем случае простой вывод в консоль с нумирацией ответов, может быть расширена (например при смене запускаемой платформы) путем реализации записи статистики в базу данных, или ,например, отправки статистики на почту игроку.

LSP
	Класс CorrectAnswerUser наследуется от AnswerUser, в реализации метода ShowStatistics(IGamePlatform platform) осуществляется вызов метода GetAnswer() для каждого элемента коллекции, благодаря реализации дочернего класса CorrectAnswerUser возможна замена дочернего на базовый класс и наоборот, 

ISP
	IGameStarter, IGameConfogurator - интерфейсы разделены, но сложно придумать ситуации когда игра не должна будет стартовать и не должна иметь настройки, только если во время тестирования(например тестирование получения конфигурации из какого либо ресурса, когда запуск игры не требуется...)
	IGameStatistics - интерфейс вынесен в отдельный для мозможности создания экземпляра игры в которой не требуется статистики (наша реализация статистику требует).

DIP
	В стартовом классе Programm реализована инверсия зависимостей,метод ConfigurateApp(IHostBuilder builder) регистрирует зависимости интерфейсов от классов реализации данных интерфейсов, для конкретного экземпляра приложения.




На примере реализации игры «Угадай число» продемонстрировать практическое применение SOLID принципов.
Программа рандомно генерирует число, пользователь должен угадать это число. При каждом вводе числа программа пишет больше или меньше отгадываемого. Кол-во попыток отгадывания и диапазон чисел должен задаваться из настроек.
В отчёте написать, что именно сделано по каждому принципу.
Приложить ссылку на проект и написать, сколько времени ушло на выполнение задачи.
