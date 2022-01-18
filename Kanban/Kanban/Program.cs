using System;
using System.IO;
using System.Reflection;
using Application;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Persistence;
using SimpleInjector;
using TrelloApi;

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
                    .AddEnvironmentVariables()
                    // .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "config.json"), true)
                    .Build());
            container.Register(() =>
                new TrelloClient(container.GetInstance<IConfiguration>().GetSection("token").Value,
                    container.GetInstance<IConfiguration>().GetSection("api-key").Value));
            container.RegisterApplications();
            container.RegisterCommands();
            container.Register(() => new DbContextOptionsBuilder<KanbanDbContext>()
                .UseNpgsql(container.GetInstance<IConfiguration>().GetSection("connectionString").Value)
                .Options);
            container.Register<KanbanDbContext>(Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(Repository<>));
            container.Register<IDateTimeProvider, StandardDateTimeProvider>();
            container.Register<TelegramBot>();
            container.RegisterInitializer<TelegramBot>(bot => bot.Start());

            container.Register<ChatInteractor>();
            container.Register<ExecutorInteractor>();
            return container;
        }

        private static void RegisterCommands(this Container container) =>
            container.Collection.Register<ICommand>(Assembly.GetCallingAssembly());

        private static void RegisterApplications(this Container container) =>
            container.Collection.Register<IApplication>(typeof(IApplication).Assembly);
    }
}