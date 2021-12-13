using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application;
using Domain;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kanban
{
    public class StartCommand : ICommand
    {
        private readonly CreateBoardInteractor _createBoardInteractor;

        public StartCommand(CreateBoardInteractor createBoardInteractor) =>
            _createBoardInteractor = createBoardInteractor;

        public string Name => @"/start";

        public async Task ExecuteAsync(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var boardName = message.Text.Split(' ', 2)[1];
            var board = new Board(boardName, new List<Card>(), new Dictionary<Guid, AccessRights>(),
                new List<State>());
            await _createBoardInteractor.CreateBoardAsync(board);
            await botClient.SendTextMessageAsync(chatId, $"Created {board.Name}!");
        }
    }
}