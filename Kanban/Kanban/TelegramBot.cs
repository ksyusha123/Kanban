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
            _client = new TelegramBotClient(Program._configuration.GetSection("botToken").Value);
        }

        public void Start()
        {
            _client.StartReceiving();
            _client.OnMessage += ClientOnMessage;
            Console.ReadLine();
            _client.StopReceiving();
        }

        private async void ClientOnMessage(object sender, MessageEventArgs e)
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