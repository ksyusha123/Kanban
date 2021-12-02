using System;
using Domain;

namespace Application
{
    public class DeleteTaskInteractor
    {
        private readonly IRepository<Task> _taskRepository;
        private readonly IRepository<Table> _tableRepository;

        public DeleteTaskInteractor(IRepository<Task> taskRepository, IRepository<Table> tableRepository)
            => (_taskRepository, _tableRepository) = (taskRepository, tableRepository);

        public async System.Threading.Tasks.Task DeleteTaskAsync(Guid taskId, Guid tableId)
        {
            var task = await _taskRepository.GetAsync(taskId);
            var table = await _tableRepository.GetAsync(tableId);
            
            table.RemoveTask(task);
            await _taskRepository.DeleteAsync(task);
            await _tableRepository.UpdateAsync(table);
        }
    }
}