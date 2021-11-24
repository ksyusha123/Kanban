using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace Kanban
{
    public class TelegramBot
    {
        private readonly TelegramBotClient _botClient;
        private static List<ICommand> commandsList;

        public TelegramBot()
        {
            _botClient = new TelegramBotClient("2082708776:AAFcnwKk7Fh0HNibqfVkaNIMQ5Z0-cqIT_4");
            _botClient.StartReceiving();
            _botClient.OnMessage += BotClientOnOnMessage;
        }

        private async void BotClientOnOnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            await _botClient.SendTextMessageAsync(message.Chat.Id, message.Text);
        }
        // public static IReadOnlyList<ICommand> Commands => commandsList.AsReadOnly();

        // public static TelegramBotClient GetBotClientAsync()
        // {
        //     if (botClient != null)
        //         return botClient;
        //
        //     commandsList = new List<ICommand> {new StartCommand()};
        //     botClient = new TelegramBotClient("2082708776:AAFcnwKk7Fh0HNibqfVkaNIMQ5Z0-cqIT_4");
        //     return botClient;
        // }
    }
}