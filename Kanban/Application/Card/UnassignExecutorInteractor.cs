using System;
using Domain;

namespace Application
{
    public class UnassignExecutorInteractor
    {
        private readonly IRepository<Card, Guid> _cardRepository;

        public UnassignExecutorInteractor(IRepository<Card, Guid> cardRepository) =>
            _cardRepository = cardRepository;

        public async System.Threading.Tasks.Task AssignAsync(Guid taskId)
        {
            var task = await _cardRepository.GetAsync(taskId);
            task.Executor = null;
            await _cardRepository.UpdateAsync(task);
        }
    }
}