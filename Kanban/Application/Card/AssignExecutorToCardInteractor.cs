using System;
using Domain;

namespace Application
{
    public class AssignExecutorToCardInteractor
    {
        private readonly IRepository<Executor, Guid> _executorRepository;
        private readonly IRepository<Card, Guid> _cardRepository;

        public AssignExecutorToCardInteractor(IRepository<Executor, Guid> executorRepository, IRepository<Card, Guid> taskRepository) =>
            (_executorRepository, _cardRepository) = (executorRepository, taskRepository);

        public async System.Threading.Tasks.Task AssignAsync(Guid executorId, Guid taskId)
        {
            var executor = await _executorRepository.GetAsync(executorId);
            var task = await _cardRepository.GetAsync(taskId);

            task.Executor = executor;
            await _cardRepository.UpdateAsync(task);
        }
    }
}