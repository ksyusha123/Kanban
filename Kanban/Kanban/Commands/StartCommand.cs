using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public class StartCommand : ICommand
    {
        private readonly string _supportedApps;

        public StartCommand(IEnumerable<IApplication> apps) =>
            _supportedApps = string.Join(", ", apps.Select(a => a.Name));

        public string Name => "/start";
        public string Help => "выводит текст просто так";
        public bool NeedBoard => false;
        public bool NeedReply => false;
        public string Hint => null;

        private string Text => "Этот бот поможет вам и вашей команде быстрее работать с Kanban доской. " +
                               $"Поддерживается использование {_supportedApps}.\n" +
                               "Перед тем, как начать работу, добавьте доску. " +
                               "Для этого используйте команду /addboard, чтобы добавить существующую доску или " +
                               "/createboard, чтобы создать новую, или же наберите команду /help, чтобы узнать " +
                               "о прочих возможностях";

        public async Task ExecuteAsync(Chat chat, Message message, ITelegramBotClient botClient) =>
            await botClient.SendTextMessageAsync(message.Chat.Id, Text);
    }
}