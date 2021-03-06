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
    public class ChangeColumnsCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public ChangeColumnsCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);

        public string Name => "/changecolumns";
        public string Help => "меняет существущие колонки";
        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение с перечислением новых названий колонок через " +
                              "символ переноса строки\n" +
                              "Пример:\nПервая колонка\nВторая колонка\nТретья колонка";

        public async Task ExecuteAsync(Chat chat, Message message, ITelegramBotClient botClient)
        {
            var newColumns = message.ReplyToMessage.Text
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();

            var board = await _apps[chat.App].BoardInteractor.GetBoardAsync(chat.BoardId);

            var columnsSet = newColumns.ToHashSet();
            foreach (var c in board.CardsByColumns)
                if (c.Any() && !columnsSet.Contains(c.Key.Name))
                {
                    await botClient.SendTextMessageAsync(chat.Id, "Невозможно выполнить команду! " +
                                                                  $"Меняя колонки, вы удалите колонку {c.Key.Name}, " +
                                                                  "в которой есть задачи");
                    return;
                }

            await _apps[chat.App].BoardInteractor.ChangeColumnsAsync(chat.BoardId, newColumns);
            await botClient.SendTextMessageAsync(chat.Id, "Поменял колонки");
        }
    }
}