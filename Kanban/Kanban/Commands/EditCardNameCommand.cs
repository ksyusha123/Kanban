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
    public class EditCardNameCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public EditCardNameCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);
        public string Name => "/editcardname";
        public bool NeedBoard => true;
        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var oldCardName = message.ReplyToMessage.Text;
            var card = (await _apps[chat.App].CardInteractor.GetCardsAsync(oldCardName, chat.BoardId)).SingleOrDefault();
            var newCardName = message.Text.Split(' ', 2)[1];
            await _apps[chat.App].CardInteractor.EditCardNameAsync(card.Id.ToString(), newCardName);
            await botClient.SendTextMessageAsync(chat.Id, $"Поменял {oldCardName} на {newCardName}");
        }
    }
}