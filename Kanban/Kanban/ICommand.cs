using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public interface ICommand
    {
        public string Name { get; }
        public bool NeedBoard { get; }
        public bool NeedReply { get; }
        public string Hint { get; }

        public Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient);
    }
}