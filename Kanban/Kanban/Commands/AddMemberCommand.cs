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
    public class AddMemberCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;
        private readonly ExecutorInteractor _executorInteractor;

        public AddMemberCommand(IEnumerable<IApplication> apps, ExecutorInteractor executorInteractor)
        {
            _executorInteractor = executorInteractor;
            _apps = apps.ToDictionary(a => a.App);
        }

        public string Name => "/addmember";
        public string Help => "добавляет участника в доску проекта";
        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение с перечислением пар: ник_в_телеграме ник_в_приложении\n" +
                              "Пример:\n" +
                              "@user1 user1\n" +
                              "@user2 user2\n";

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var membersToAdd = message.ReplyToMessage.Text.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => (p.Split(' ')[0].TrimStart('@'), p.Split(' ')[1]))
                .ToList();
            var boardInteractor = _apps[chat.App].BoardInteractor;
            var telegramUsernames = new List<string>();
            foreach (var (telegramUsername, appUsername) in membersToAdd)
            {
                await _executorInteractor.AddExecutorAsync(appUsername, telegramUsername);
                await boardInteractor.AddMemberAsync(chat.BoardId, appUsername);
                telegramUsernames.Add(telegramUsername);
            }
            await botClient.SendTextMessageAsync(chat.Id, $"Добавил {string.Join(' ', telegramUsernames)} на доску");
        }
    }
}