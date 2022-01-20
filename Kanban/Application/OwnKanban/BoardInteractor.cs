using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.OwnKanban
{
    public class BoardInteractor : IBoardInteractor
    {
        private readonly IRepository<Board> _boardRepository;
        private readonly IRepository<Card> _cardRepository;
        private readonly IRepository<Column> _columnRepository;
        private readonly ExecutorInteractor _executorInteractor;

        public BoardInteractor(IRepository<Board> boardRepository, IRepository<Card> cardRepository,
            IRepository<Column> columnRepository, ExecutorInteractor executorInteractor) =>
            (_boardRepository, _cardRepository, _columnRepository, _executorInteractor) =
            (boardRepository, cardRepository, columnRepository, executorInteractor);

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
            var oldColumnsDict = board.Columns.ToDictionary(c => c.Id, c => c.Name);
            var oldColumns = board.Columns.Select(c => c).ToArray();

            foreach (var card in board.Cards) card.ColumnId = newColumnsDict[oldColumnsDict[card.ColumnId]].Id;

            board.Columns.Clear();
            board.Columns.AddRange(columns);

            await _columnRepository.DeleteAsync(oldColumns);
            await _boardRepository.UpdateAsync(board);
        }

        public async Task<IEnumerable<Column>> GetAllColumnsAsync(string boardId) =>
            (await _boardRepository.GetAsync(boardId)).Columns;

        public async Task AddMemberAsync(string boardId, string userId) => 
            await _executorInteractor.AddExecutorAsync(userId);

        public async Task DeleteBoardAsync(string boardId)
        {
            var board = await _boardRepository.GetAsync(boardId);
            await _boardRepository.DeleteAsync(board);
        }
    }
}