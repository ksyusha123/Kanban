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
    public class AddMeCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;
        private readonly ExecutorInteractor _executorInteractor;

        public AddMeCommand(IEnumerable<IApplication> apps, ExecutorInteractor executorInteractor)
        {
            _executorInteractor = executorInteractor;
            _apps = apps.ToDictionary(a => a.App);
        }
        public string Name => "/addme";

        public string Help => "то же самое, что и /addmember, но не нужно указывать свой ник";

        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недосаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение со своим ником в сторннем приложении\n" +
                              "Пример: user1";
        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var appId = message.ReplyToMessage.Text.Trim();
            var sender = message.From.Username;
            await _executorInteractor.AddExecutorAsync(appId, sender);
            await _apps[chat.App].BoardInteractor.AddMemberAsync(chat.BoardId, appId);
            await botClient.SendTextMessageAsync(chat.Id, $"Добавил {sender} на доску");
        }
    }
}