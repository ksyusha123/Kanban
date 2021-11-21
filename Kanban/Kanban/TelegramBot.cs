using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace Kanban
{
    public class TelegramBot
    {
        private static TelegramBotClient botClient;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static TelegramBotClient GetBotClientAsync()
        {
            if (botClient != null)
                return botClient;

            commandsList = new List<Command> {new StartCommand()};
            botClient = new TelegramBotClient("2082708776:AAFcnwKk7Fh0HNibqfVkaNIMQ5Z0-cqIT_4");
            return botClient;
        }
    }

    public class StartCommand : Command
    {
        public override string Name => @"/start";

        public override bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            return message.Text.Contains(this.Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, "Hallo I'm ASP.NET Core Bot");
        }
    }
}