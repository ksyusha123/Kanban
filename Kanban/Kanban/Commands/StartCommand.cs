using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kanban
{
    public class StartCommand : ICommand
    {
        public string Name => @"/start";

        public async Task Execute(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, "Hello, it's start command");
        }
    }
}