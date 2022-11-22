using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using NLog;
using AdminGroupBot.Configuration;

namespace AdminGroupBot.TelegramBot
{
    internal class BotStarter
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly MessagesResolver resolver = new MessagesResolver();

        private readonly ITelegramBotClient botClient = new TelegramBotClient(Config.EnvironmentVariables["TELEGRAM_BOT_TOKEN"]);
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
            await resolver.Resolve(botClient, update);
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            logger.Error(errorMessage);
            return Task.CompletedTask;
        }
    }
}
