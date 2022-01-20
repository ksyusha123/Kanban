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
    class ViewCommentCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public ViewCommentCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);
        public string Name => "/viewcomments";
        public string Help => "посмотреть все комментарии к карточке";
        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение с названием карточки\n" +
                              "Пример: повторить матан";
        public async Task ExecuteAsync(Chat chat, Message message, ITelegramBotClient botClient)
        {
            var cardName = message.ReplyToMessage.Text.Trim();
            var cardInteractor = _apps[chat.App].CardInteractor;
            var card = await cardInteractor.GetCard(cardName, chat.BoardId);
            var comments = (await cardInteractor.GetComments(card.Id))
                .Select(c => c.ToString());
            await botClient.SendTextMessageAsync(chat.Id,
                $"Комментарии к карточке {card.Name}:\n\n" + string.Join('\n', comments));
        }
    }
}