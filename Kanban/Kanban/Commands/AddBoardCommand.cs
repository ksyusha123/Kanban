using Chat = Domain.Chat;

namespace Kanban
{
    public class AddBoardCommand : ICommand
    {
        private readonly IRepository<Chat, long> _chatRepository;
        private readonly IEnumerable<IApplication> _apps;

        public AddBoardCommand(IRepository<Chat, long> chatRepository, IEnumerable<IApplication> apps) =>
            (_chatRepository, _apps) = (chatRepository, apps);
        public string Name => "/addboard";
        public string Help => "Добавляет существующую доску в бот";
        public bool NeedBoard => false;
        public bool NeedReply => true;

        public string Hint => "Недостаточно аргументов :(\n" +
                               "Ответьте этой командой на сообщение вида: приложение идентификатор_доски\n" +
                               "Пример: trello 123456789101112131415160";

        public async Task ExecuteAsync(Chat chat1, Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var chat = await _chatRepository.GetAsync(chatId);

            if (chat is { })
            {
                await botClient.SendTextMessageAsync(chatId, "у вас уже есть доска!!!");
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

            chat = new Chat(chatId, app, splitted[1]);
            await _chatRepository.AddAsync(chat);
            await botClient.SendTextMessageAsync(chatId, $"я добавиль {splitted[1]}!");
        }
    }
}