using Chat = Domain.Chat;

namespace Kanban
{
    public class GetAllColumnsCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public GetAllColumnsCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);
        public string Name => "/getallcolumns";
        public string Help => "Вывести все колонки";
        public bool NeedBoard => true;
        public bool NeedReply => false;
        public string Hint { get; }

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var boardInteractor = _apps[chat.App].BoardInteractor;
            var columns = await boardInteractor.GetAllColumnsAsync(chat.BoardId);
            await botClient.SendTextMessageAsync(chat.Id,
                string.Join('\n', columns.OrderBy(c => c.OrderNumber).Select(c => c.Name)));
        }
    }
}