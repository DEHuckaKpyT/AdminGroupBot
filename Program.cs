using AdminGroupBot.Config;
using AdminGroupBot.TelegramBot;
using NLog;


internal class Program
{
    private readonly static Logger logger = LogManager.GetCurrentClassLogger();
    private readonly static BotStarter bot = new BotStarter();

    public static void Main(string[] args)
    {
        logger.Info("Starting program.");
        List<string> messagePatterns = AppConfig.Bot.MessagePatterns;

        bot.StartBot();

        Console.ReadKey();

        bot.StopBot();
    }
}
