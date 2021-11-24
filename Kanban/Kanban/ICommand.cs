using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kanban
{
    public interface ICommand
    {
        public string Name { get; }

        public Task Execute(Message message, TelegramBotClient botClient);

        // public bool Contains(Message message);
    }
}