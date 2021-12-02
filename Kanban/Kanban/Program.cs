using System;
using System.IO;
using System.Reflection;
using Application;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
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
            container.Register<TelegramBot>();
            container.RegisterInitializer<TelegramBot>(bot => bot.Start());
            // container.Register<IExecutor, Executor>();  // здесь нужен guid
            // container.Register<ITask, Domain.Task>();
            // container.Register<EditTaskInteractor>();   // здесь нужен repository
            // container.Register<CreateTaskInteractor>();
            // container.Register<CreateProjectInteractor>();
            // container.Register<AddTableToProjectInteractor>();
            // container.Register<ChangeTaskStateInteractor>();
            // container.Register<AssignExecutorToTaskInteractor>();
            return container;
        }
    }
}
