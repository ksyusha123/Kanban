using System;
using Domain;

namespace Application
{
    public class AddTeamMemberInteractor
    {
        private readonly IRepository<Board, Guid> _boardRepository;
        private readonly IRepository<Executor, Guid> _executorRepository;

        public AddTeamMemberInteractor(IRepository<Board, Guid> tableRepository, IRepository<Executor, Guid> executorRepository)
            => (_boardRepository, _executorRepository) = (tableRepository, executorRepository);

        public async System.Threading.Tasks.Task AddTeamMemberAsync(Guid executorId, Guid tableId)
        {
            var executor = await _executorRepository.GetAsync(executorId);
            var table = await _boardRepository.GetAsync(tableId);
            
            table.AddExecutor(executor);
            
            await _boardRepository.UpdateAsync(table);
        }
    }
}