using AdminGroupBot.TelegramBot;
using NLog;


internal class Program
{
    private readonly static Logger logger = LogManager.GetCurrentClassLogger();
    private readonly static BotConfig bot = new BotConfig();

    public static void Main(string[] args)
    {
        logger.Info("Starting program.");

        bot.StartBot();

        Console.ReadKey();

        bot.StopBot();
    }
}
