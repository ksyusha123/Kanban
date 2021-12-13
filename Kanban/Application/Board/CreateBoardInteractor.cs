using System;
using Domain;

namespace Application
{
    public class CreateBoardInteractor
    {
        private readonly IRepository<Board, Guid> _boardRepository;
        public CreateBoardInteractor(IRepository<Board, Guid> boardRepository) => _boardRepository = boardRepository;

        public async System.Threading.Tasks.Task CreateBoardAsync(Board board)
        {
            await _boardRepository.AddAsync(board);
        }
    }
}