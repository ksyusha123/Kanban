using Domain;

namespace Application
{
    public class CreateTableInteractor
    {
        private readonly IRepository<Table> _tableRepository;
        public CreateTableInteractor(IRepository<Table> tableRepository) => _tableRepository = tableRepository;

        public async System.Threading.Tasks.Task CreateTableAsync(Table table)
        {
            await _tableRepository.AddAsync(table);
        }
    }
}