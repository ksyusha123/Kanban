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
    public class GetAllColumnsCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public GetAllColumnsCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);
        public string Name => "/getallcolumns";
        public string Help => "выводит все колонки";
        public bool NeedBoard => true;
        public bool NeedReply => false;
        public string Hint => null;

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var boardInteractor = _apps[chat.App].BoardInteractor;
            var columns = await boardInteractor.GetAllColumnsAsync(chat.BoardId);
            await botClient.SendTextMessageAsync(chat.Id,
                string.Join('\n', columns.OrderBy(c => c.OrderNumber).Select(c => c.Name)));
        }
    }
}