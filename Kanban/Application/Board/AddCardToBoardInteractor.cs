using System;
using Domain;

namespace Application
{
    public class AddCardToBoardInteractor
    {
        private readonly IRepository<Card, Guid> _cardRepository;
        private readonly IRepository<Board, Guid> _boardRepository;

        public AddCardToBoardInteractor(IRepository<Card, Guid> cardRepository, IRepository<Board, Guid> boardRepository)
        => (_cardRepository, _boardRepository) = (cardRepository, boardRepository);

        public async System.Threading.Tasks.Task AddCardToBoardAsync(Guid cardId, Guid boardId)
        {
            var card = await _cardRepository.GetAsync(cardId);
            var board = await _boardRepository.GetAsync(boardId);
            
            board.AddTask(card);
            await _boardRepository.UpdateAsync(board);
        }
    }
}