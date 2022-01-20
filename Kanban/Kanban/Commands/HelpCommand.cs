using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public class HelpCommand : ICommand
    {
        public HelpCommand(IEnumerable<ICommand> commands) =>
            _text = string.Join('\n', commands.Append(this).Select(c => $"{c.Name} - {c.Help}"));

        public string Name => "/help";
        public string Help => "выводит данную подсказку";
        public bool NeedBoard => false;
        public bool NeedReply => false;
        public string Hint => null!;

        private readonly string _text;

        public async Task ExecuteAsync(Chat chat, Message message, ITelegramBotClient botClient) =>
            await botClient.SendTextMessageAsync(message.Chat.Id, _text);
    }
}