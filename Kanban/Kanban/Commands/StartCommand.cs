﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Kanban
{
    public class StartCommand : ICommand
    {
        private readonly string _supportedApps;

        public StartCommand(IEnumerable<IApplication> apps) =>
            _supportedApps = string.Join(", ", apps.Select(a => a.Name));

        public string Name => "/start";
        public bool NeedBoard => false;
        public bool NeedReply => false;
        public string Hint { get; }

        private string Text => "Этот бот поможет вам и вашей команде быстрее работать с Kanban доской. " +
                               $"Поддерживается использование {_supportedApps}.\n" +
                               "Наберите команду /help чтобы посмотреть, что он может";

        public async Task ExecuteAsync(Chat chat, Message message, TelegramBotClient botClient) =>
            await botClient.SendTextMessageAsync(message.Chat.Id, Text);
    }
}