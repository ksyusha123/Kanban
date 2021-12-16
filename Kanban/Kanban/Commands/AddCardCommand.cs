using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Domain;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;
using Task = System.Threading.Tasks.Task;

namespace Kanban
{
    public class AddCardCommand : ICommand
    {
        private readonly IRepository<Chat, long> _chatRepository;
        private readonly IEnumerable<IApplication> _apps;
        public AddCardCommand(IRepository<Chat, long> chatRepository, IEnumerable<IApplication> apps) => 
            (_chatRepository, _apps) = (chatRepository, apps);

        public string Name => "/addCard";
        public async Task ExecuteAsync(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var chat = await _chatRepository.GetAsync(chatId);
            var app = chat.App;
            var boardInteractor = _apps.First(i => i.App == app).BoardInteractor;
            var cardInteractor = _apps.First(i => i.App == app).CardInteractor;
            var cardName = message.Text.Split(' ', 2)[1] != string.Empty
                ? message.Text.Split(' ', 2)[1]
                : message.ReplyToMessage.Text;
            var card = await cardInteractor.CreateCardAsync(cardName);
            await boardInteractor.AddCardAsync(card.Id.ToString(), chat.BoardId);
        }
    }
}