using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kanban
{
    public class CreateBoardCommand : ICommand
    {
        private readonly ChatInteractor _chatInteractor;
        private readonly IEnumerable<IApplication> _apps;

        public CreateBoardCommand(ChatInteractor chatInteractor, IEnumerable<IApplication> apps) =>
            (_chatInteractor, _apps) = (chatInteractor, apps);

        public string Name => "/createboard";

        public async Task ExecuteAsync(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var chat = await _chatInteractor.GetChatAsync(chatId);

            if (chat is { })
            {
                await botClient.SendTextMessageAsync(chatId, "у вас уже есть доска!!!");
                return;
            }

            var splitted = message.Text.Split(' ', 3);
            var app = splitted[1].ToLower() == "trello" ? App.Trello : App.OwnKanban;
            var boardInteractor = _apps.First(i => i.App == app).BoardInteractor;
            var board = await boardInteractor.CreateBoardAsync(splitted[2]);

            await _chatInteractor.AddChatAsync(chatId, app, board.Id.ToString());
            await botClient.SendTextMessageAsync(chatId, $"я сделаль {board.Name}!");
        }
    }
}