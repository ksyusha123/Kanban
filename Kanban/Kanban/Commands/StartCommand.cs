using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain;
using Persistence;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public class StartCommand : ICommand
    {
        private readonly IRepository<Chat, long> _chatRepository;
        private readonly IEnumerable<IApplication> _apps;

        public StartCommand(IRepository<Chat, long> chatRepository, List<IApplication> apps) => 
            (_chatRepository, _apps) = (chatRepository, apps);

        public string Name => "/start";

        private string Text => "Этот бот поможет вам и вашей команде быстрее работать с kanban доской. " +
                               "Наберите команду /help чтобы посмотреть, что он может";

        public async Task ExecuteAsync(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, Text);
        }
    }
}