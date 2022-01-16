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
    class DeleteBoardCommand : ICommand
    {
        private readonly ChatInteractor _chatInteractor;
        private readonly IDictionary<App, IApplication> _apps;

        public DeleteBoardCommand(ChatInteractor chatInteractor, IEnumerable<IApplication> apps)
        {
            _chatInteractor = chatInteractor;
            _apps = apps.ToDictionary(a => a.App);
        }
        public string Name => "/deleteboard";

        public string Help => "удаляет доску из бота. " +
                              "Если вы удалили доску из стороннего приложения сами, " +
                              "то рекомендуется также воспользоваться этой командой.";

        public bool NeedBoard => true;
        public bool NeedReply => false;
        public string Hint { get; }
        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient)
        {
            var app = _apps[chat.App];
            try
            {
                await app.BoardInteractor.DeleteBoardAsync(chat.BoardId);
            }
            finally
            {
                await _chatInteractor.DeleteChatAsync(chat.Id);
            }

            await botClient.SendTextMessageAsync(message.Chat.Id, "Удалил доску");
        }
    }
}