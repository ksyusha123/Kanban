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
    public class ChangeColumnCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public ChangeColumnCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);

        public string Name => "/changecolumn";

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            if (chat is null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Не найдена доска проекта. Сначала введите /addboard или /help");
                return;
            }

            var cardName = message.ReplyToMessage.Text;
            var app = _apps[chat.App];
            var card = (await app.CardInteractor.GetCardsAsync(cardName, chat.BoardId)).SingleOrDefault();
            if (card is null)
            {
                // не найдено карточек или их несколько. надо шото написать юзерам. возможно, стоит разделить 2 случая:
                // в случае нескольких карт вывести их список и попросить уточнить,
                // а в случае, когда ничего не нашли просто сказать об этом
            }

            var columnName = message.Text.Split(' ', 2)[1];
            var column = default(Column); // find column by name (можно скопипастить из CardInteractor.GetCardsAsync)
            await app.CardInteractor.ChangeState(card!.Id.ToString(), column);
            await botClient.SendTextMessageAsync(chat.Id, $"Передвинул карточку {card.Name} в колонку {column.Name}");
        }
    }
}