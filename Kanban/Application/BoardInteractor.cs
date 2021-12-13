using System;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public class BoardInteractor : IBoardInteractor
    {
        private readonly IRepository<Board, Guid> _boardRepository;
        private readonly IRepository<Card, Guid> _cardRepository;
        
        public BoardInteractor(IRepository<Board, Guid> boardRepository, IRepository<Card, Guid> cardRepository) =>
            (_boardRepository, _cardRepository) = (boardRepository, cardRepository);

        public async Task CreateBoardAsync(string name)
        {
            var board = new Board(name);
            await _boardRepository.AddAsync(board);
        }

        public async Task AddCardAsync(string cardId, string boardId)
        {
            var card = await _cardRepository.GetAsync(new Guid(cardId));
            var board = await _boardRepository.GetAsync(new Guid(boardId));
            board.AddTask(card);
            await _boardRepository.UpdateAsync(board);
        }

        public async Task DeleteCardAsync(string cardId, string boardId)
        {
            var card = await _cardRepository.GetAsync(new Guid(cardId));
            var board = await _boardRepository.GetAsync(new Guid(boardId));
            board.RemoveTask(card);
            await _cardRepository.DeleteAsync(card);
            await _boardRepository.UpdateAsync(board);
        }
    }
}