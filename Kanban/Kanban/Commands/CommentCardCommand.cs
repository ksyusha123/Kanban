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
    public class CommentCardCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;
        private readonly ExecutorInteractor _executorInteractor;

        public CommentCardCommand(IEnumerable<IApplication> apps, ExecutorInteractor executorInteractor)
        {
            _executorInteractor = executorInteractor;
            _apps = apps.ToDictionary(a => a.App);
        }

        public string Name => "/comment";
        public string Help => "оставляет комментарий к задаче";
        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение вида:\n" +
                              "название_карточки\n" +
                              "комментарий\n" +
                              "Пример:\n" +
                              "сделать методы асинхронными\n" +
                              "через Task.Run()";
        public async Task ExecuteAsync(Chat chat, Message message, ITelegramBotClient botClient)
        {
            var splitted = message.ReplyToMessage.Text.Split('\n');
            if (splitted.Length < 2)
            {
                await botClient.SendTextMessageAsync(chat.Id, Hint);
                return;
            }

            var app = _apps[chat.App];
            var cardName = splitted[0].Trim();
            var card = await app.CardInteractor.GetCard(cardName, chat.BoardId);
            if (card is null)
            {
                await botClient.SendTextMessageAsync(chat.Id,
                    $"Я не нашёл карточку {cardName} :(\n" +
                    "Воспользуйтесь командой /findcard, чтобы уточнить запрос");
                return;
            }

            var comment = splitted[1].Trim();
            await _apps[chat.App].CardInteractor.AddComment(card.Id, comment, message.From.Username);
            await botClient.SendTextMessageAsync(chat.Id, $"Добавил комментарий {comment} к карточке {cardName}");
        }
    }
}