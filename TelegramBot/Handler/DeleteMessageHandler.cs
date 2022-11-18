using static AdminGroupBot.Configuration.Config;
using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Text.RegularExpressions;

namespace AdminGroupBot.TelegramBot.Handler
{
    internal class DeleteMessageHandler : IMessageHandler
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly List<Regex> messageRegexes = Bot.MessagePatterns
            .ConvertAll(pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

        public bool IsMustBeExecuted(Update update)
        {
            if (update.Message is not { } message) return false;

            if (message.Text is not { } messageText) return false;

            if (IsNotMatchAll(messageText)) return false;

            return true;
        }

        public async Task Execute(long chatId, Update update, ITelegramBotClient botClient)
        {
            if (update.Message is not { } message) return;

            logger.Info($"deleting message \"{message.Text}\" from @{message.Chat.Username}");
            await botClient.DeleteMessageAsync(chatId, message.MessageId);
        }

        private bool IsNotMatchAll(string text)
        {
            foreach(Regex regex in messageRegexes)
            {
                if (regex.IsMatch(text)) return false;
            }

            return true;
        }
    }
}
