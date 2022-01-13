using System;
using System.Collections.Generic;
using Application;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Kanban
{
    public class TelegramBot
    {
        private readonly TelegramBotClient _client;
        private readonly Dictionary<string, ICommand> _commands;
        private readonly ChatInteractor _chatInteractor;

        public TelegramBot(IConfiguration configuration, IEnumerable<ICommand> commands, ChatInteractor chatInteractor)
        {
            _client = new TelegramBotClient(configuration.GetSection("botToken").Value);
            _commands = FillCommandsDictionary(commands);
            _chatInteractor = chatInteractor;
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

            if (commandFull is null || !commandFull.StartsWith('/')) return;

            var commandSplitted = commandFull.Split(' ', 2);

            try
            {
                if (_commands.TryGetValue(commandSplitted[0], out var command))
                {
                    var chat = await _chatInteractor.GetChatAsync(message.Chat.Id);
                    if (command.NeedBoard && chat is null)
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id,
                            "Не найдена доска проекта. Сначала введите /addboard или /help");
                        return;
                    }

                    if (command.NeedReply && message.ReplyToMessage is null)
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, command.Hint);
                        return;
                    }
                    await command.ExecuteAsync(chat, message, _client);
                }
                else
                    await _client.SendTextMessageAsync(message.Chat, "Таких команд не учил!");
            }
            catch (Exception ex)
            {
                await _client.SendTextMessageAsync(message.Chat, "Ошибочка вышла!");
                Console.WriteLine(ex);
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