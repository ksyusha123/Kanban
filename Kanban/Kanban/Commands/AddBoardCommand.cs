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
    public class AddBoardCommand : ICommand
    {
        private readonly ChatInteractor _chatInteractor;
        private readonly Dictionary<App, IApplication> _apps;

        public AddBoardCommand(ChatInteractor chatInteractor, IEnumerable<IApplication> apps) =>
            (_chatInteractor, _apps) = (chatInteractor, apps.ToDictionary(a => a.App));

        public string Name => "/addboard";
        public string Help => "добавляет существующую доску в бот\n" +
                              "Если доска приватная в стороннем приложении, то сначала добавьте superfiitbot@gmail.com на доску.\n" +
                              "Чтобы все команды работали корректно, выдайте боту права администратора.";
        public bool NeedBoard => false;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                              "Ответьте этой командой на сообщение вида: приложение идентификатор_доски\n" +
                              "Пример: trello 123456789101112131415160";

        public async Task ExecuteAsync(Chat chat1, Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id.ToString();
            var chat = await _chatInteractor.GetChatAsync(chatId);

            if (chat is { })
            {
                await botClient.SendTextMessageAsync(chatId, "У вас уже есть доска");
                return;
            }

            var splitted = message.ReplyToMessage.Text.Split(' ', 2,
                StringSplitOptions.RemoveEmptyEntries);

            if (splitted.Length < 2)
            {
                await botClient.SendTextMessageAsync(chatId, Hint);
                return;
            }

            var app = splitted[0].ToLower() == "trello" ? App.Trello : App.OwnKanban;

            Board board;
            try
            {
                board = await _apps[app].BoardInteractor.GetBoardAsync(splitted[1]);
            }
            catch (System.Net.WebException)
            {
                await botClient.SendTextMessageAsync(chatId, $"Не нашел такую доску :(\n" +
                                                             $"Проверьте приложение и id\n" +
                                                             $"Возможно, вы забыли добавить superfiitbot@gmail.com на приватную доску");
                return;
            }

            await _chatInteractor.AddChatAsync(chatId, app, board.Id);
            await botClient.SendTextMessageAsync(chatId, $"Я добавил {board.Name}\n" +
                                                         $"Удачи в создании проекта!");
        }
    }
}