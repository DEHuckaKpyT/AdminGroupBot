using AdminGroupBot.TelegramBot.Handler;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace AdminGroupBot.TelegramBot
{
    internal class MessagesResolver
    {
        List<IMessageHandler> handlers = new List<IMessageHandler>()
        {
            // обработчики событий бота
            new DeleteMessageHandler()
        };

        public async Task Resolve(ITelegramBotClient botClient, Update update)
        {
            if (update.Message is not { } message) return;

            foreach (IMessageHandler handler in handlers)
            {
                if (handler.IsMustBeExecuted(update))
                {
                    await handler.Execute(message.Chat.Id, update, botClient);
                }
            }
        }
    }
}
