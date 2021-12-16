using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kanban
{
    public interface ICommand
    {
        public string Name { get; }

        public Task ExecuteAsync(Message message, TelegramBotClient botClient);
    }
}