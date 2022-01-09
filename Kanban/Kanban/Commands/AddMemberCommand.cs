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
    public class AddMemberCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public AddMemberCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);
        public string Name => "/addmember";
        public bool NeedBoard => true;
        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var membersToAdd = message.ReplyToMessage.Text.Split(' ');
            var boardInteractor = _apps[chat.App].BoardInteractor;
            foreach (var member in membersToAdd) 
                await boardInteractor.AddMemberAsync(chat.BoardId, member);
            await botClient.SendTextMessageAsync(chat.Id, $"Добавил {string.Join(' ', membersToAdd)} на доску");
        }
    }
}