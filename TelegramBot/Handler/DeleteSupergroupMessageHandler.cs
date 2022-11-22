
using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Text.RegularExpressions;
using AdminGroupBot.Configuration;

namespace AdminGroupBot.TelegramBot.Handler
{
    internal class DeleteSupergroupMessageHandler : IMessageHandler
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly List<Regex> removeMessageRegexes = Config.Bot.RemoveMessagePatterns
            .ConvertAll(pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

        public bool IsMustBeExecuted(Update update)
        {
            if (update.Message is not { } message) return false;

            if (message.Chat.Type != ChatType.Supergroup) return false;

            if (Config.Bot.WhiteListIds.Contains(message.From.Id)) return false;

            if (Config.Bot.BlackListIds.Contains(message.From.Id)) return true;

            if (message.Text is not { } messageText) return false;

            if (IsNotMatchAll(messageText)) return false;

            return true;
        }

        public async Task Execute(long chatId, Update update, ITelegramBotClient botClient)
        {
            if (update.Message is not { } message) return;

            try
            {
                await botClient.DeleteMessageAsync(chatId, message.MessageId);
                logger.Info($"Message \"{message.Text}\" from {message.From.FirstName} ({message.From.Id}) deleted.");
            }
            catch (Exception ex)
            {
                logger.Error($"Message \"{message.Text}\" from {message.From.FirstName} ({message.From.Id}) can't be deleted.");
                logger.Error(ex);
            }
        }

        private bool IsNotMatchAll(string text)
        {
            foreach (Regex regex in removeMessageRegexes)
            {
                if (regex.IsMatch(text)) return false;
            }

            return true;
        }
    }
}
