using System;
using System.Collections.Generic;
using Application;
using Domain;
using Telegram.Bot;
using Telegram.Bot.Types;
using Task = System.Threading.Tasks.Task;

namespace Kanban
{
    public class AddCardCommand : ICommand
    {
        private readonly IRepository<Board, Guid> _repository;
        public AddCardCommand(IRepository<Board, Guid> repository) => _repository = repository;

        public string Name => "/addCard";
        public async Task ExecuteAsync(Message message, TelegramBotClient botClient)
        {
            // await _repository.AddAsync(new Board(new List<Domain.Task>(), new Dictionary<Guid, AccessRights>(),
            //     new List<State>())); 
        }
    }
}