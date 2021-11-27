using System;
using Domain;

namespace Application
{
    public class AddTeamMemberInteractor
    {
        private readonly IRepository<Table> _tableRepository;
        private readonly IRepository<Executor> _executorRepository;

        public AddTeamMemberInteractor(IRepository<Table> tableRepository, IRepository<Executor> executorRepository)
            => (_tableRepository, _executorRepository) = (tableRepository, executorRepository);

        public async System.Threading.Tasks.Task AddTeamMemberAsync(Guid executorId, Guid tableId)
        {
            var executor = await _executorRepository.GetAsync(executorId);
            var table = await _tableRepository.GetAsync(tableId);
            
            table.AddExecutor(executor);
            
            await _tableRepository.UpdateAsync(table);
        }
    }
}