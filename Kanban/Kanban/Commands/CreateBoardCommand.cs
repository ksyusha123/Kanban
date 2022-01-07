using System;
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
        private readonly ChatInteractor _chatInteractor;
        private readonly IDictionary<App, IApplication> _apps;

        public CreateBoardCommand(ChatInteractor chatInteractor, IEnumerable<IApplication> apps)
        {
            _chatInteractor = chatInteractor;
            _apps = apps.ToDictionary(a => a.App);
        }

        public string Name => "/createboard";
        public bool NeedBoard => false;

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            if (chat is { })
            {
                await botClient.SendTextMessageAsync(chat.Id, "У вас уже есть доска!");
                return;
            }

            var chatId = message.Chat.Id;
            var splitted = message.Text.Split(' ', 3);
            if (!Enum.TryParse(splitted[1], true, out App app))
            {
                await botClient.SendTextMessageAsync(chatId, $"Мы не поддерживаем {splitted[1]}. Подробнее - /help");
                return;
            }
            
            var boardInteractor = _apps[app].BoardInteractor;
            var board = await boardInteractor.CreateBoardAsync(splitted[2]);

            await _chatInteractor.AddChatAsync(chatId, app, board.Id.ToString());
            await botClient.SendTextMessageAsync(chatId, $"Я создал доску {board.Name}. Удачи в создании проекта!");
        }
    }
}