using System;
using Domain;

namespace Application
{
    public class AddTaskToTableInteractor
    {
        private readonly IRepository<Task> _taskRepository;
        private readonly IRepository<Table> _tableRepository;

        public AddTaskToTableInteractor(IRepository<Task> taskRepository, IRepository<Table> tableRepository)
        => (_taskRepository, _tableRepository) = (taskRepository, tableRepository);

        public async System.Threading.Tasks.Task AddTaskToTableAsync(Guid taskId, Guid tableId)
        {
            var task = await _taskRepository.GetAsync(taskId);
            var table = await _tableRepository.GetAsync(tableId);
            
            table.AddTask(task);
            await _tableRepository.UpdateAsync(table);
        }
    }
}