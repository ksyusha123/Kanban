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
    public class DeleteCardCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public DeleteCardCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);
        public string Name => "/deletecard";
        public string Help => "удаляет карточку";
        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение с частью названия карточки, " +
                              "которую вы хотите удалить\n" +
                              "Пример: повторить";

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var cardName = message.ReplyToMessage.Text.Trim();
            var app = _apps[chat.App];
            var card = await app.CardInteractor.GetCard(cardName, chat.BoardId);
            if (card is null)
            {
                await botClient.SendTextMessageAsync(chat.Id,
                    $"Я не нашёл карточку {cardName} :(\n" +
                    "Воспользуйтесь командой /findcard, чтобы уточнить запрос");
                return;
            }
            await app.BoardInteractor.DeleteCardAsync(card.Id, chat.BoardId);
            await botClient.SendTextMessageAsync(chat.Id, $"Удалил карточку {card.Name}");
        }
    }
}