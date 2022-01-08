﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public class BoardInteractor : IBoardInteractor
    {
        private readonly IRepository<Board, string> _boardRepository;
        private readonly IRepository<Card, string> _cardRepository;

        public BoardInteractor(IRepository<Board, string> boardRepository, IRepository<Card, string> cardRepository) =>
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

        public async Task<Board> GetBoardAsync(string boardId) => await _boardRepository.GetAsync(boardId);

        public async Task DeleteCardAsync(string cardId, string boardId)
        {
            var card = await _cardRepository.GetAsync(cardId);
            var board = await _boardRepository.GetAsync(boardId);
            board.RemoveCard(card);
            await _cardRepository.DeleteAsync(card);
            await _boardRepository.UpdateAsync(board);
        }

        public async Task ChangeColumnsAsync(string boardId, string[] newColumnsNames)
        {
            var board = await _boardRepository.GetAsync(boardId);

            var columns = newColumnsNames.Select((c, i) => new Column(c, i)).ToList();
            var newColumnsDict = columns.ToDictionary(c => c.Name);
            var oldColumnsDict = board.Columns.ToDictionary(c => c.Id);

            foreach (var card in board.Cards) card.ColumnId = newColumnsDict[oldColumnsDict[card.ColumnId].Name].Id;
            board.Columns = columns;
            await _boardRepository.UpdateAsync(board);
        }

        public async Task<IEnumerable<Column>> GetAllColumnsAsync(string boardId) =>
            (await _boardRepository.GetAsync(boardId)).Columns;
    }
}