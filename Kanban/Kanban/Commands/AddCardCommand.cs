using System.Collections.Generic;
using System.Linq;
using Application;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;
using Task = System.Threading.Tasks.Task;

namespace Kanban
{
    public class AddCardCommand : ICommand
    {
        private readonly ChatInteractor _chatInteractor;
        private readonly IEnumerable<IApplication> _apps;
        public AddCardCommand(ChatInteractor chatInteractor, IEnumerable<IApplication> apps) => 
            (_chatInteractor, _apps) = (chatInteractor, apps);

        public string Name => "/addcard";
        public async Task ExecuteAsync(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var chat = await _chatInteractor.GetChatAsync(chatId);
            var app = chat.App;
            var cardInteractor = _apps.First(i => i.App == app).CardInteractor;
            var strings = message.Text.Split(' ', 2);
            var cardName = strings.Length > 1
                ? strings[1]
                : message.ReplyToMessage.Text;
            var card = await cardInteractor.CreateCardAsync(cardName, chat.BoardId);
            await botClient.SendTextMessageAsync(chatId, $"слышь ты! я добавиль {cardName}! работай, дедлайны горят, а ты лежишь!");
        }
    }
}