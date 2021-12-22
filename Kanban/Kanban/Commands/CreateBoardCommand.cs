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
    public class CreateBoardCommand : ICommand
    {
        private readonly IRepository<Chat, long> _chatRepository;
        private readonly IEnumerable<IApplication> _apps;

        public CreateBoardCommand(IRepository<Chat, long> chatRepository, IEnumerable<IApplication> apps) =>
            (_chatRepository, _apps) = (chatRepository, apps);

        public string Name => "/createboard";

        public async Task ExecuteAsync(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var chat = await _chatRepository.GetAsync(chatId);

            if (chat is { })
            {
                await botClient.SendTextMessageAsync(chatId, "у вас уже есть доска!!!");
                return;
            }

            var splitted = message.Text.Split(' ', 3);
            var app = splitted[1].ToLower() == "trello" ? App.Trello : App.OwnKanban;
            var boardInteractor = _apps.First(i => i.App == app).BoardInteractor;
            var board = await boardInteractor.CreateBoardAsync(splitted[2]);

            chat = new Chat(chatId, app, board.Id.ToString());
            await _chatRepository.AddAsync(chat);
            await botClient.SendTextMessageAsync(chatId, $"я сделаль {board.Name}!");
        }
    }
}