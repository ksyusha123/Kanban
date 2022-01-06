using System;
using System.Collections.Generic;
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

        public async Task<Board> CreateBoardAsync(string name)
        {
            var todoColumn = new Column("ToDo", 0);
            var wipColumn = new Column("Work In Progress", 1);
            var doneColumn = new Column("Done", 2);

            var board = new Board(name, new List<Column> {todoColumn, wipColumn, doneColumn});
            await _boardRepository.AddAsync(board);
            return board;
        }

        public async Task DeleteCardAsync(string cardId, string boardId)
        {
            var card = await _cardRepository.GetAsync(new Guid(cardId));
            var board = await _boardRepository.GetAsync(new Guid(boardId));
            board.RemoveCard(card);
            await _cardRepository.DeleteAsync(card);
            await _boardRepository.UpdateAsync(board);
        }

        public async Task<IEnumerable<Column>> GetAllColumnsAsync(string boardId)
        {
            var board = await _boardRepository.GetAsync(new Guid(boardId));
            return board.Columns;
        }
    }
}