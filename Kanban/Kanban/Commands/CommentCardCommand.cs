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

        public CommentCardCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);
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
        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var splitted = message.ReplyToMessage.Text.Split('\n');
            if (splitted.Length < 2)
            {
                await botClient.SendTextMessageAsync(chat.Id, Hint);
                return;
            }

            var app = _apps[chat.App];
            var card = await app.CardInteractor.GetCard(splitted[0], chat.BoardId);
            if (card is null)
            {
                await botClient.SendTextMessageAsync(chat.Id,
                    $"Я не нашёл карточку {splitted[0]} :(\n" +
                    "Воспользуйтесь командой /findcard, чтобы уточнить запрос");
                return;
            }
        }
    }
}