using System;
using Domain;

namespace Application
{
    public class CreateTableInteractor
    {
        private readonly IRepository<Table, Guid> _tableRepository;
        public CreateTableInteractor(IRepository<Table, Guid> tableRepository) => _tableRepository = tableRepository;

        public async System.Threading.Tasks.Task CreateTableAsync(Table table)
        {
            await _tableRepository.AddAsync(table);
        }
    }
}