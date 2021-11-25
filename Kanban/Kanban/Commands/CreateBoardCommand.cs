using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kanban
{
    public class CreateBoardCommand : ICommand
    {
        public string Name => "/createBoard";
        public Task Execute(Message message, TelegramBotClient botClient)
        {
            throw new System.NotImplementedException();
        }
    }
}