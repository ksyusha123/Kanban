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
        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                               "Ответьте этой командой на сообщение с частью названия карточки, которую вы хотите удалить\n" +
                               "Пример: повторить";
        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var cardName = message.ReplyToMessage.Text.Trim();
            var card = (await _apps[chat.App].CardInteractor.GetCardsAsync(cardName, chat.BoardId)).FirstOrDefault();
            await _apps[chat.App].BoardInteractor.DeleteCardAsync(card.Id, chat.BoardId);
            await botClient.SendTextMessageAsync(chat.Id, $"Удалил карточку {card.Name}");
        }
    }
}