﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace Kanban
{
    public class TelegramBot
    {
        private readonly TelegramBotClient _client;
        private readonly Dictionary<string, ICommand> _commands;

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
            var commandFull = message.Text;
            var commandSplitted = commandFull?.Split(' ', 2);

            try
            {
                if (commandFull!.StartsWith('/'))
                {
                    if (commandSplitted != null && _commands.TryGetValue(commandSplitted[0], out var command))
                        await command.ExecuteAsync(message, _client);
                    else
                        await _client.SendTextMessageAsync(message.Chat, "таких команд не учил!");
                }
            }
            catch (Exception)
            {
                await _client.SendTextMessageAsync(message.Chat, "ошибочка вышла!");
            }
        }

        private static Dictionary<string, ICommand> FillCommandsDictionary(IEnumerable<ICommand> commands)
        {
            var dict = new Dictionary<string, ICommand>();
            foreach (var command in commands)
            {
                dict[command.Name] = command;
                dict[$"{command.Name}@AgileBoardBot"] = command;
            }

            return dict;
        }
    }
}