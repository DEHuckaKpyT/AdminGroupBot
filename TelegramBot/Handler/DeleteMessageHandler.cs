using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdminGroupBot.TelegramBot.Handler
{
    internal class DeleteMessageHandler : IMessageHandler
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly string str = "asd";

        public bool IsMustBeExecute(Update update)
        {
            if (update.Message is not { } message) return false;

            if (message.Text is not { } messageText) return false;

            if (messageText != str) return false;

            return true;
        }

        public async Task Execute(long chatId, Update update, ITelegramBotClient botClient)
        {
            if (update.Message is not { } message) return;

            logger.Info($"deleting message \"{message.Text}\" from @{message.Chat.Username}");
            await botClient.DeleteMessageAsync(chatId, message.MessageId);
        }
    }
}
