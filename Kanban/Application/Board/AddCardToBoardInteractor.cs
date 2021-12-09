using System;
using Domain;

namespace Application
{
    public class AddCardToBoardInteractor
    {
        private readonly IRepository<Card, Guid> _cardRepository;
        private readonly IRepository<Board, Guid> _boardRepository;

        public AddCardToBoardInteractor(IRepository<Card, Guid> taskRepository, IRepository<Board, Guid> tableRepository)
        => (_cardRepository, _boardRepository) = (taskRepository, tableRepository);

        public async System.Threading.Tasks.Task AddTaskToTableAsync(Guid taskId, Guid tableId)
        {
            var task = await _cardRepository.GetAsync(taskId);
            var table = await _boardRepository.GetAsync(tableId);
            
            table.AddTask(task);
            await _boardRepository.UpdateAsync(table);
        }
    }
}