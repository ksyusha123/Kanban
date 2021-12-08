using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using SimpleInjector;

namespace Kanban
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureContainer().GetInstance<TelegramBot>();
        }

        private static Container ConfigureContainer()
        {
            var container = new Container();
            container.RegisterSingleton<IConfiguration>(() => 
                new ConfigurationBuilder()
                    .AddJsonFile(
                        Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName,
                            "config.json"), 
                        true)
                    .Build());
            RegisterCommands(container);
            container.Register((() => new DbContextOptionsBuilder()));
            container.Register(() =>
            {
                var options = new DbContextOptions<KanbanDbContext>();
                return new KanbanDbContext(options);
            });
            container.Register<Repository<Board, Guid>>();
            container.Register<IRepository<Board, Guid>, Repository<Board, Guid>>();
            container.Register<TelegramBot>();
            container.RegisterInitializer<TelegramBot>(bot => bot.Start());
            return container;
        }

        private static void RegisterCommands(Container container)
        {
            var commands = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsInstanceOfType(typeof(ICommand)));
            container.Collection.Register<ICommand>(Assembly.GetCallingAssembly());
        }
    }
}
