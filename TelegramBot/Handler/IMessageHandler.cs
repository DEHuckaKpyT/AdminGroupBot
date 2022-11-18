using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdminGroupBot.TelegramBot.Handler
{
    internal interface IMessageHandler
    {
        bool IsMustBeExecuted(Update update);

        Task Execute(long chatId, Update update, ITelegramBotClient botClient);
    }
}
