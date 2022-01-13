using Chat = Domain.Chat;

namespace Kanban
{
    public class GetColumnCardsCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public GetColumnCardsCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);

        public string Name => "/getcolumncards";
        public string Help => "Получает все карточки из данной колонки";
        public bool NeedBoard => true;
        public bool NeedReply => true;
        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение с названием колонки\n" +
                              "Пример: ToDo";

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var columnName = message.ReplyToMessage.Text.Trim();
            var board = await _apps[chat.App].BoardInteractor.GetBoardAsync(chat.BoardId);
            var column = board.Columns.SingleOrDefault(c => c.Name == columnName);

            if (column is null)
            {
                await botClient.SendTextMessageAsync(chat.Id, $"Нет столбца {columnName}");
                return;
            }

            var cards = board.CardsByColumns[column].ToArray();
            if (!cards.Any())
            {
                await botClient.SendTextMessageAsync(chat.Id, "Нет карточек в столбце");
                return;
            }

            await botClient.SendTextMessageAsync(chat.Id,
                $"Карточки в столбце {columnName}:\n\n" + string.Join('\n', cards.Select(c => $"- {c.Name}")));
        }
    }
}