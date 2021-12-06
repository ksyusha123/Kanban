using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kanban
{
    public class AddCardCommand : ICommand
    {
        public string Name => "/addCard";
        public Task Execute(Message message, TelegramBotClient botClient)
        {
            throw new System.NotImplementedException();
        }
    }
}