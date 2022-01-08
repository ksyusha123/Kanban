using System;
using Domain;

namespace Application
{
    public class AddTeamMemberInteractor
    {
        private readonly IRepository<Board, string> _boardRepository;
        private readonly IRepository<Executor, Guid> _executorRepository;

        public AddTeamMemberInteractor(IRepository<Board, string> boardRepository, IRepository<Executor, Guid> executorRepository)
            => (_boardRepository, _executorRepository) = (boardRepository, executorRepository);

        // public async System.Threading.Tasks.Task AddTeamMemberAsync(Guid executorId, Guid boardId)
        // {
        //     var executor = await _executorRepository.GetAsync(executorId);
        //     var board = await _boardRepository.GetAsync(boardId);
        //     
        //     board.AddExecutor(executor);
        //     
        //     await _boardRepository.UpdateAsync(board);
        // }
    }
}