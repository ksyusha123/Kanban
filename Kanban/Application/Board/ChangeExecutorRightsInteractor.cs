using System;
using Domain;

namespace Application
{
    public class ChangeExecutorRightsInteractor
    {
        private readonly IRepository<Board, Guid> _boardRepository;
        private readonly IRepository<Executor, Guid> _executorRepository;

        public ChangeExecutorRightsInteractor(IRepository<Board, Guid> tableRepository, IRepository<Executor, Guid> executorRepository)
            => (_boardRepository, _executorRepository) = (tableRepository, executorRepository);

        public async System.Threading.Tasks.Task GiveRightsToExecutorAsync(Guid tableId, Guid executorId, AccessRights accessRights)
        {
            var table = await _boardRepository.GetAsync(tableId);
            var executor = await _executorRepository.GetAsync(executorId);
            
            table.ChangeExecutorRights(executor, accessRights);
            
            await _boardRepository.UpdateAsync(table);
        }
    }
}