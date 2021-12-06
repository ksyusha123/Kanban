using System;
using Domain;

namespace Application
{
    public class CreateTableInteractor
    {
        private readonly IRepository<Board, Guid> _tableRepository;
        public CreateTableInteractor(IRepository<Board, Guid> tableRepository) => _tableRepository = tableRepository;

        public async System.Threading.Tasks.Task CreateTableAsync(Board board)
        {
            await _tableRepository.AddAsync(board);
        }
    }
}