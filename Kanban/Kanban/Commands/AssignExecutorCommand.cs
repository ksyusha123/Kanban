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
    public class AssignExecutorCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;
        private readonly ExecutorInteractor _executorInteractor;

        public AssignExecutorCommand(IEnumerable<IApplication> apps, ExecutorInteractor executorInteractor)
        {
            _executorInteractor = executorInteractor;
            _apps = apps.ToDictionary(a => a.App);
        }

        public string Name => "/assignexecutor";
        public string Help => "назначает исполнителя задачи";
        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение вида\n" +
                              "название_карточки\n" +
                              "ник_исполнителя_задачи_в_телеграме\n" +
                              "Пример:\n" +
                              "пофиксить changecolumns\n" +
                              "@Themplarer";
        public async Task ExecuteAsync(Chat chat, Message message, ITelegramBotClient botClient)
        {
            var splitted = message.ReplyToMessage.Text.Split('\n')
                .Select(t => t.Trim())
                .ToList();
            if (splitted.Count < 2)
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

            var executor = await _executorInteractor.GetExecutor(splitted[1].TrimStart('@'));
            await app.CardInteractor.AssignCardExecutorAsync(card.Id, executor.Id);
            await botClient.SendTextMessageAsync(chat.Id, $"Теперь задачу {card.Name} должен делать {splitted[1]}");
        }
    }
}