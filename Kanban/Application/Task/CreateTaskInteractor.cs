using Domain;

namespace Application
{
    public class CreateTaskInteractor
    {
        private readonly IRepository<Task> _taskRepository;
        public CreateTaskInteractor(IRepository<Task> taskRepository) => _taskRepository = taskRepository;

        public async System.Threading.Tasks.Task CreateTaskAsync(Task task)
        {
            await _taskRepository.AddAsync(task);
        }
    }
}