using Chat = Domain.Chat;

namespace Kanban
{
    public interface ICommand
    {
        public string Name { get; }
        string Help { get; }
        public bool NeedBoard { get; }
        public bool NeedReply { get; }
        public string Hint { get; }

        Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient);
    }
}