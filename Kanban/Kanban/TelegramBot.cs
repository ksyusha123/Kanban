using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace Kanban
{
    public class TelegramBot
    {
        private readonly TelegramBotClient _client;
        private static Dictionary<string, ICommand> _commands = new()
        {
            {"/start@AgileBoardBot", new StartCommand()}, 
            {"/help@AgileBoardBot", new HelpCommand()},
            {"/start", new StartCommand()},
            {"/help", new HelpCommand()}
        };

        public TelegramBot()
        {
            _client = new TelegramBotClient("2082708776:AAFcnwKk7Fh0HNibqfVkaNIMQ5Z0-cqIT_4");
        }

        public void Start()
        {
            _client.StartReceiving();
            _client.OnMessage += ClientOnOnMessage;
            Console.ReadLine();
            _client.StopReceiving();
        }

        private async void ClientOnOnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            var command = message.Text;
            if (_commands.ContainsKey(command))
            {
                await _commands[command].Execute(message, _client);
            }
        }
    }
}