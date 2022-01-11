﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public class GetColumnCardsCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public GetColumnCardsCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);

        public string Name => "/getcolumncards";
        public bool NeedBoard => true;

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var columnName = message.ReplyToMessage.Text;
            var board = await _apps[chat.App].BoardInteractor.GetBoardAsync(chat.BoardId);
            var column = board.Columns.SingleOrDefault(c => c.Name == columnName);

            if (column is null)
            {
                await botClient.SendTextMessageAsync(chat.Id, $"Нет столбца {columnName}");
                return;
            }

            var cards = board.CardsByColumns[column].ToArray();
            if (!cards.Any())
            {
                await botClient.SendTextMessageAsync(chat.Id, "Нет карточек в столбце");
                return;
            }

            await botClient.SendTextMessageAsync(chat.Id,
                $"Карточки в столбце {columnName}:\n\n" + string.Join('\n', cards.Select(c => $"- {c.Name}")));
        }
    }
}