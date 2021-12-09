using System;
using Domain;

namespace Application
{
    public class EditCardInteractor
    {
        private readonly IRepository<Card, Guid> _cardRepository;

        public EditCardInteractor(IRepository<Card, Guid> cardRepository) => _cardRepository = cardRepository;

        public async System.Threading.Tasks.Task EditTaskAsync(Guid taskId, string name = null, string description = null)
        {
            var task = await _cardRepository.GetAsync(taskId);

            if (name != null) task.Name = name;

            if (description != null) task.Description = description;
            
            await _cardRepository.UpdateAsync(task);
        }
    }
}