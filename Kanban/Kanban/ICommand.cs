using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public interface ICommand
    {
        public string Name { get; }

        public Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient);
    }
}