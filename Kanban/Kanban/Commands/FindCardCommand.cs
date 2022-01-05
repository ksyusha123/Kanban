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
    public class FindCardCommand : ICommand
    {
        private readonly IDictionary<App, IApplication> _apps;

        public FindCardCommand(IEnumerable<IApplication> apps) => _apps = apps.ToDictionary(a => a.App);
        public string Name => "/findcard";

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            if (chat is null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Не найдена доска проекта. Сначала введите /addboard или /help");
                return;
            }

            var strings = message.Text.Split(' ', 2);
            var cards = (await _apps[chat.App].CardInteractor.GetCardsAsync(strings[1], chat.BoardId)).ToArray();

            if (!cards.Any())
            {
                await botClient.SendTextMessageAsync(chat.Id, "Не нашёл карточек по вашему запросу :(");
                return;
            }

            await botClient.SendTextMessageAsync(chat.Id,
                "Результаты поиска:\n\n" + string.Join('\n', cards.Select(c => $"{c.Name} в {c.Column.Name}")));
        }
    }
}