using Chat = Domain.Chat;

namespace Kanban
{
    public class MoveCardCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public MoveCardCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);

        public string Name => "/movecard";
        public string Help => "Переместить карточку в другую колонку";
        public bool NeedBoard => true;
        public bool NeedReply => true;
        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение вида:\n" +
                              "часть_названия_карточки\n" +
                              "название_колонки\n" +
                              "Пример:\n" +
                              "матан\n" +
                              "Готово";

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var splitted = message.ReplyToMessage.Text.Split("\n");
            if (splitted.Length < 2)
            {
                await botClient.SendTextMessageAsync(chat.Id, Hint);
                return;
            }
            var cardName = splitted[0];
            var app = _apps[chat.App];
            var card = (await app.CardInteractor.GetCardsAsync(cardName, chat.BoardId)).SingleOrDefault();
            if (card is null)
            {
                // не найдено карточек или их несколько. надо шото написать юзерам. возможно, стоит разделить 2 случая:
                // в случае нескольких карт вывести их список и попросить уточнить,
                // а в случае, когда ничего не нашли просто сказать об этом
                return;
            }

            var columnName = splitted[1];
            var column = (await app.BoardInteractor.GetAllColumnsAsync(chat.BoardId))
                .FirstOrDefault(c => c.Name == columnName);
            if (column is null)
            {
                await botClient.SendTextMessageAsync(chat.Id, "Столбец не найден!");
                return;
            }

            await app.CardInteractor.ChangeColumnAsync(card.Id, column);
            await botClient.SendTextMessageAsync(chat.Id, $"Передвинул карточку {card.Name} в колонку {column.Name}");
        }
    }
}