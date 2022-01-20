using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public interface ICommand
    {
        string Name { get; }
        string Help { get; }
        bool NeedBoard { get; }
        bool NeedReply { get; }
        string Hint { get; }

        Task ExecuteAsync(Chat chat, Message message, ITelegramBotClient botClient);
    }
}