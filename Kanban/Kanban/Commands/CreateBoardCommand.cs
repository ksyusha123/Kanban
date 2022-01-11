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
            var splitted = message.ReplyToMessage.Text.Split(' ', 2);
            if (!Enum.TryParse(splitted[0], true, out App app))
            {
                await botClient.SendTextMessageAsync(chatId, $"Мы не поддерживаем {splitted[0]}. Подробнее - /help");
                return;
            }

            var boardInteractor = _apps[app].BoardInteractor;
            var board = await boardInteractor.CreateBoardAsync(splitted[1]);

            await _chatInteractor.AddChatAsync(chatId, app, board.Id);
            await botClient.SendTextMessageAsync(chatId, $"Я создал доску {board.Name} со столбцами " +
                                                         $"{string.Join(", ", board.Columns.Select(c => c.Name))}. " +
                                                         $"Id доски: {board.Id}. " +
                                                         "Если вы создали доску в стороннем приложении, добавьте людей с помощью /addmember. " +
                                                         "Удачи в создании проекта!");
        }
    }
}