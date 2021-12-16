using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Application;
using Domain;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public class CreateBoardCommand : ICommand
    {
        private readonly IRepository<Chat, long> _chatRepository;
        private readonly IEnumerable<IApplication> _apps;

        public CreateBoardCommand(IRepository<Chat, long> chatRepository, IEnumerable<IApplication> apps) => 
            (_chatRepository, _apps) = (chatRepository, apps);
        
        public string Name => "/createBoard";
        
        public async Task ExecuteAsync(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var chat = await _chatRepository.GetAsync(chatId);
            var app = chat.App;
            var boardInteractor = _apps.First(i => i.App == app).BoardInteractor;
            var boardName = message.Text.Split(' ', 2)[1];
            await boardInteractor.CreateBoardAsync(boardName);
            await botClient.SendTextMessageAsync(chatId, $"я сделаль {boardName}!");
        }
    }
}