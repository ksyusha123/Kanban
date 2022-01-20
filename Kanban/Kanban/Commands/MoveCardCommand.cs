using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public class MoveCardCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public MoveCardCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);

        public string Name => "/movecard";
        public string Help => "перемещает карточку в другую колонку";
        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение вида:\n" +
                              "название_карточки\n" +
                              "название_колонки\n" +
                              "Пример:\n" +
                              "матан\n" +
                              "Готово";

        public async Task ExecuteAsync(Chat chat, Message message, ITelegramBotClient botClient)
        {
            var splitted = message.ReplyToMessage.Text.Split("\n");
            if (splitted.Length < 2)
            {
                await botClient.SendTextMessageAsync(chat.Id, Hint);
                return;
            }

            var cardName = splitted[0].Trim();
            var app = _apps[chat.App];
            var card = await app.CardInteractor.GetCard(cardName, chat.BoardId);
            if (card is null)
            {
                await botClient.SendTextMessageAsync(chat.Id,
                    $"Я не нашёл карточку {cardName} :(\n" +
                    "Воспользуйтесь командой /findcard, чтобы уточнить запрос");
                return;
            }
            var columnName = splitted[1].Trim();
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