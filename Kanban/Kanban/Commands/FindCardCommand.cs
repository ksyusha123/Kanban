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
    public class FindCardCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public FindCardCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);
        public string Name => "/findcard";
        public string Help => "ищет карточку на доске по части названия";
        public bool NeedBoard => true;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение с частью названия карточки\n" +
                              "Пример: матан";

        public async Task ExecuteAsync(Chat chat, Message message, ITelegramBotClient botClient)
        {
            var nameTokens = message.ReplyToMessage.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim());
            var cards = (await _apps[chat.App].CardInteractor.GetCardsAsync(nameTokens, chat.BoardId)).ToArray();

            if (!cards.Any())
            {
                await botClient.SendTextMessageAsync(chat.Id, "Не нашёл карточек по вашему запросу :(");
                return;
            }

            var columns = (await _apps[chat.App].BoardInteractor.GetAllColumnsAsync(chat.BoardId))
                .ToDictionary(c => c.Id);
            await botClient.SendTextMessageAsync(chat.Id,
                "Результаты поиска:\n\n" +
                string.Join('\n', cards.Select(c => $"{c.Name} в {columns[c.ColumnId].Name}")));
        }
    }
}