using System.Collections.Generic;
using System.Threading.Tasks;
using Application;
using Domain;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public class AddBoardCommand : ICommand
    {
        private readonly IRepository<Chat, long> _chatRepository;
        private readonly IEnumerable<IApplication> _apps;

        public AddBoardCommand(IRepository<Chat, long> chatRepository, IEnumerable<IApplication> apps) =>
            (_chatRepository, _apps) = (chatRepository, apps);
        public string Name => "/addboard";
        public bool NeedBoard => false;

        public async Task ExecuteAsync(Chat chat1, Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var chat = await _chatRepository.GetAsync(chatId);

            if (chat is { })
            {
                await botClient.SendTextMessageAsync(chatId, "у вас уже есть доска!!!");
                return;
            }

            var splitted = message.ReplyToMessage.Text.Split(' ', 2);
            var app = splitted[0].ToLower() == "trello" ? App.Trello : App.OwnKanban;

            chat = new Chat(chatId, app, splitted[2]);
            await _chatRepository.AddAsync(chat);
            await botClient.SendTextMessageAsync(chatId, $"я добавиль {splitted[1]}!");
        }
    }
}