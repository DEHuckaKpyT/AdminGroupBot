using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdminGroupBot.TelegramBot.Handler
{
    internal interface IMessageHandler
    {
        bool IsMustBeExecute(Update update);

        Task Execute(long chatId, Update update, ITelegramBotClient botClient);
    }
}
