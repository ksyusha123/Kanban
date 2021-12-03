using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace Kanban
{
    public class TelegramBot
    {
        private readonly TelegramBotClient _client;
        private static Dictionary<string, ICommand> _commands;

        public TelegramBot(IConfiguration configuration)
        {
            _client = new TelegramBotClient(configuration.GetSection("botToken").Value);
            _commands = FillCommandsDictionary();
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

        private static Dictionary<string, ICommand> FillCommandsDictionary()
        {
            var dict = new Dictionary<string, ICommand>();
            var commands = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)));
            foreach (var command in commands)
            {
                var commandInstance = (ICommand) Activator.CreateInstance(command);
                // ReSharper disable PossibleNullReferenceException
                var commandName = command
                    .GetProperty("Name")
                    .GetValue(commandInstance)
                    .ToString();
                // ReSharper disable once AssignNullToNotNullAttribute
                dict[commandName] = commandInstance;
                dict[$"{commandName}@AgileBoardBot"] = commandInstance;
            }

            return dict;
        }
    }
}