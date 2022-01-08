using System;
using Domain;

namespace Application
{
    public class ChangeExecutorRightsInteractor
    {
        private readonly IRepository<Board, string> _boardRepository;
        private readonly IRepository<Executor, Guid> _executorRepository;

        public ChangeExecutorRightsInteractor(IRepository<Board, string> boardRepository, IRepository<Executor, Guid> executorRepository)
            => (_boardRepository, _executorRepository) = (boardRepository, executorRepository);

        // public async System.Threading.Tasks.Task GiveRightsToExecutorAsync(Guid boardId, Guid executorId, AccessRights accessRights)
        // {
        //     var board = await _boardRepository.GetAsync(boardId);
        //     var executor = await _executorRepository.GetAsync(executorId);
        //     
        //     board.ChangeExecutorRights(executor, accessRights);
        //     
        //     await _boardRepository.UpdateAsync(board);
        // }
    }
}