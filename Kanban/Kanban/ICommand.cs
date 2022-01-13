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

        Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient);
    }
}