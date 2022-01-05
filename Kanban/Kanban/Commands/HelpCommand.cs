using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public class HelpCommand : ICommand
    {
        public string Name => "/help";
        private const string Text = "<place for help>";

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient) =>
            await botClient.SendTextMessageAsync(chat.Id, Text);
    }
}