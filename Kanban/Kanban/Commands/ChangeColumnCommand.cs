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
    public class ChangeColumnCommand : ICommand
    {
        private readonly ChatInteractor _chatInteractor;
        private readonly IEnumerable<IApplication> _apps;

        public ChangeColumnCommand(ChatInteractor chatInteractor, IEnumerable<IApplication> apps) =>
            (_chatInteractor, _apps) = (chatInteractor, apps);

        public string Name => "/changecolumn";
        public async Task ExecuteAsync(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var chat = await _chatInteractor.GetChatAsync(chatId);
            var columnName = message.Text.Split(' ', 2)[1];
            var cardName = message.ReplyToMessage.Text;
            Card card; // find card by cardname
            var app = _apps.First(a => a.App == chat.App);
            var cardInteractor = app.CardInteractor;
            var boardInteractor = app.BoardInteractor;
            Column column; // find column by name
            await cardInteractor.ChangeState(card.Id, column);
            await botClient.SendTextMessageAsync(chatId, "передвинул твою карточку, а мог бы и сам справиться!");
        }
    }
}