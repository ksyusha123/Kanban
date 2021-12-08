using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application;
using Domain;
using Microsoft.Extensions.Configuration;
using Persistence;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace Kanban
{
    public class TelegramBot
    {
        private readonly TelegramBotClient _client;
        private static Dictionary<string, ICommand> _commands;

        public TelegramBot(IConfiguration configuration, IEnumerable<ICommand> commands)
        {
            _client = new TelegramBotClient(configuration.GetSection("botToken").Value);
            _commands = FillCommandsDictionary(commands);
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
            if (command != null && _commands.ContainsKey(command))
                await _commands[command].ExecuteAsync(message, _client);
        }

        private static Dictionary<string, ICommand> FillCommandsDictionary(IEnumerable<ICommand> commands)
        {
            var dict = new Dictionary<string, ICommand>();
            foreach (var command in commands)
            {
                var commandName = command
                    .GetType()
                    .GetProperty("Name")
                    ?.GetValue(command)
                    ?.ToString();
                dict[commandName!] = command;
                dict[$"{commandName}@AgileBoardBot"] = command;
            }

            return dict;
        }
    }
}