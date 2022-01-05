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
    public class AddCardCommand : ICommand
    {
        private readonly Dictionary<App, IApplication> _apps;

        public AddCardCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);

        public string Name => "/addcard";

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            if (chat is null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Не найдена доска проекта. Сначала введите /addboard или /help");
                return;
            }

            var cardInteractor = _apps[chat.App].CardInteractor;
            var strings = message.Text.Split(' ', 2);
            var cardName = strings.Length > 1
                ? strings[1]
                : message.ReplyToMessage.Text;
            var card = await cardInteractor.CreateCardAsync(cardName, chat.BoardId);
            await botClient.SendTextMessageAsync(chat.Id, $"Создал задачу {card.Name} в колонке {card.Column.Name}");
        }
    }
}