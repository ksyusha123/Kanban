using Chat = Domain.Chat;

namespace Kanban
{
    public class AddMemberCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public AddMemberCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);
        public string Name => "/addmember";
        public string Help => "Добавляет участника в доску проекта";
        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                               "Ответьте этой командой на сообщение с перечислением идентификаторов аккаунтов через пробел\n" +
                               "Пример: user1 user2 user3";
        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var membersToAdd = message.ReplyToMessage.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var boardInteractor = _apps[chat.App].BoardInteractor;
            foreach (var member in membersToAdd)
                await boardInteractor.AddMemberAsync(chat.BoardId, member);
            await botClient.SendTextMessageAsync(chat.Id, $"Добавил {string.Join(' ', membersToAdd)} на доску");
        }
    }
}