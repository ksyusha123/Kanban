using System;
using Domain;

namespace Application
{
    public class ChangeCardStateInteractor
    {
        private readonly IRepository<Card, Guid> _cardRepository;

        public ChangeCardStateInteractor(IRepository<Card, Guid> cardRepository) => _cardRepository = cardRepository;

        public async System.Threading.Tasks.Task ChangeTaskState(Guid taskId, State state)
        {
            var task = await _cardRepository.GetAsync(taskId);
            task.State = state;
            
            await _cardRepository.UpdateAsync(task);
        }
    }
}