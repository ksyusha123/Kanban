using System;
using System.Collections.Generic;
using Application;
using Domain;
using Infrastructure;
using Persistence;

namespace Kanban
{
    class Program
    {
        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            var designTimeDbContextFactory = new DesignTimeDbContextFactory();
            var kanbanDbContext = designTimeDbContextFactory.CreateDbContext(Array.Empty<string>());
            
            var standardDateTimeProvider = new StandardDateTimeProvider();

            var initNext = new List<State>();
            var middleNext = new List<State>();
            var state1 = new State("init", Array.Empty<State>(), initNext);
            var state2 = new State("middle", new[] {state1}, middleNext);
            var state3 = new State("final", new[] {state2}, Array.Empty<State>());
            initNext.Add(state2);
            middleNext.Add(state3);

            var executorRepository = new Repository<Executor>(kanbanDbContext);
            var jeva = new Executor("jeva", "Themplarer");
            await executorRepository.AddAsync(jeva);

            var taskRepository = new Repository<Task>(kanbanDbContext);
            var task = new Task("сделать", "1", null!, state1, standardDateTimeProvider);
            await taskRepository.AddAsync(task);

            var taskAddInteractor = new TaskAddInteractor(executorRepository, taskRepository);
            await taskAddInteractor.AssignAsync(jeva.Id, task.Id);
        }
    }
}