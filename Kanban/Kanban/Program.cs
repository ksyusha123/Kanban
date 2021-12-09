using System;
using System.IO;
using System.Reflection;
using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using SimpleInjector;

namespace Kanban
{
    internal static class Program
    {
        private static void Main(string[] args) => ConfigureContainer().GetInstance<TelegramBot>();

        private static Container ConfigureContainer()
        {
            var container = new Container();
            container.RegisterSingleton<IConfiguration>(() =>
                new ConfigurationBuilder()
                    .AddJsonFile(
                        Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName,
                            "config.json"),
                        true)
                    .Build());
            container.RegisterCommands();
            container.Register(() => new DbContextOptionsBuilder<KanbanDbContext>()
                .UseNpgsql(container.GetInstance<IConfiguration>().GetSection("connectionString").Value)
                .Options);
            container.Register<KanbanDbContext>();
            container.Register<Repository<Board, Guid>>();
            container.Register<IRepository<Board, Guid>, Repository<Board, Guid>>();
            container.Register<TelegramBot>();
            container.RegisterInitializer<TelegramBot>(bot => bot.Start());
            return container;
        }

        private static void RegisterCommands(this Container container) =>
            container.Collection.Register<ICommand>(Assembly.GetCallingAssembly());
    }
}