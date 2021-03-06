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
        public string Help => "создает новую доску и добавляет её в бот";
        public bool NeedBoard => false;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение вида: приложение название_доски\n" +
                              "Пример: trello проект";

        public async Task ExecuteAsync(Chat chat, Message message, ITelegramBotClient botClient)
        {
            if (chat is { })
            {
                await botClient.SendTextMessageAsync(chat.Id, "У вас уже есть доска!");
                return;
            }

            var chatId = message.Chat.Id.ToString();

            var splitted = message.ReplyToMessage.Text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length < 2)
            {
                await botClient.SendTextMessageAsync(chatId, Hint);
                return;
            }

            if (!Enum.TryParse(splitted[0], true, out App app))
            {
                await botClient.SendTextMessageAsync(chatId, $"Мы не поддерживаем {splitted[0]}. Подробнее - /help");
                return;
            }

            var boardInteractor = _apps[app].BoardInteractor;
            var board = await boardInteractor.CreateBoardAsync(splitted[1]);

            await _chatInteractor.AddChatAsync(chatId, app, board.Id);
            await botClient.SendTextMessageAsync(chatId,
                $"Я создал доску {board.Name} со столбцами {string.Join(", ", board.Columns.Select(c => c.Name))}. " +
                $"Id доски: {board.Id}. Добавьте людей на доску с помощью /addmember или /addme. Удачи в создании проекта!");
        }
    }
}