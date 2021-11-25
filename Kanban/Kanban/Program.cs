using System;
using Application;
using Domain;
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
            container.Register(() => new TelegramBot());
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
