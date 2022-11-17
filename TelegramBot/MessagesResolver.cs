using AdminGroupBot.TelegramBot.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace AdminGroupBot.TelegramBot
{
    internal class MessagesResolver
    {
        List<IMessageHandler> handlers = new List<IMessageHandler>()
        {
            new DeleteMessageHandler()
        };

        public async Task Resolve(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message) return;

            foreach (IMessageHandler handler in handlers)
            {
                if (handler.IsMustBeExecute(update))
                {
                    await handler.Execute(message.Chat.Id, update, botClient);
                }
            }
        }
    }
}
