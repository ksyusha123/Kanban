using Chat = Domain.Chat;

namespace Kanban
{
    public class HelpCommand : ICommand
    {
        public HelpCommand(IEnumerable<ICommand> commands)
        {
            Help = string.Join('\n', commands.Select(c => $"{c.Name} - {c.Help}"));
        }

        public string Name => "/help";
        public string Help { get; }
        public bool NeedBoard => false;
        public bool NeedReply => false;
        public string Hint { get; }

        private const string Text = "/addboard - добавляет существующую доску в бот\n" +
                                    "/createboard - создает новую доску и добавляет её в бот\n" +
                                    "/addcard - добавляет новую карточку на доску\n" +
                                    "/getallcolumns - вывести все колонки\n" +
                                    "/changecolumn - переместить карточку в другую колонку\n" +
                                    "/changecolumns - поменять существущие колонки\n" +
                                    "/findcard - дает боту частичный текст задачи для поиска задачи на доске\n" +
                                    "/deletecard - удаляет задачу\n" +
                                    "Перед тем, как начать работу, добавьте доску\n" +
                                    "Для этого используйте команду /addBoard, чтобы добавить существующую доску или /createBoard, чтобы создать новую";

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient) =>
            await botClient.SendTextMessageAsync(message.Chat.Id, Text);
    }
}