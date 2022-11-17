using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using NLog;

namespace AdminGroupBot.TelegramBot
{
    internal class BotConfig
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly MessagesResolver resolver = new MessagesResolver();

        private readonly ITelegramBotClient botClient = new TelegramBotClient("5238202878:AAED_7vuofY30YpGwP1DKxNbwc9h5vS6H2I");
        private readonly CancellationTokenSource cancellation = new CancellationTokenSource();
        private readonly ReceiverOptions receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = new UpdateType[]
            {
                UpdateType.Message,
                UpdateType.EditedMessage
            }
        };

        public async void StartBot()
        {
            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cancellation.Token
            );

            logger.Info($"Bot started listening for @{(await botClient.GetMeAsync()).Username}");
        }

        public void StopBot()
        {
            // Send cancellation request to stop bot
            cancellation.Cancel();
            cancellation.Dispose();
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await resolver.Resolve(botClient, update, cancellationToken);
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
