using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Infrastructure;
using TrelloApi;

namespace Application.Trello
{
    class TrelloBoardInteractor : IBoardInteractor
    {
        private readonly TrelloBoardClient _trelloBoardClient;
        private readonly TrelloCardClient _trelloCardClient;
        private readonly TrelloListClient _trelloListClient;
        private readonly IDateTimeProvider _dateTimeProvider;

        public TrelloBoardInteractor(TrelloClient trelloClient, IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _trelloCardClient = new TrelloCardClient(trelloClient);
            _trelloBoardClient = new TrelloBoardClient(trelloClient);
            _trelloListClient = new TrelloListClient(trelloClient);
        }

        public async Task<Board> CreateBoardAsync(string name)
        {
            var trelloBoard = await _trelloBoardClient.CreatedAsync(name);
            var columns = (await _trelloBoardClient.GetAllListsAsync(trelloBoard.Id))
                .Select(t => new Column(t.Id, t.Name, t.Pos))
                .ToList();
            return new Board(trelloBoard.Id, trelloBoard.Name, columns);
        }

        public async Task<Board> GetBoardAsync(string boardId)
        {
            var trelloBoard = await _trelloBoardClient.LoadAsync(boardId);
            var columns = (await _trelloBoardClient.GetAllListsAsync(trelloBoard.Id))
                .Select(t => new Column(t.Id, t.Name, t.Pos))
                .ToList();
            var board = new Board(boardId, trelloBoard.Name, columns);
            foreach (var column in columns)
            foreach (var card in
                (await _trelloListClient.GetAllCardsAsync(column.Id))
                .Select(c => new Card(c.Id, c.Name, c.Desc,
                    column.Id, _dateTimeProvider)))
                board.AddCard(card);
            return board; 
        }

        public async Task DeleteCardAsync(string cardId, string boardId) => await _trelloCardClient.DeleteAsync(cardId);

        public async Task ChangeColumnsAsync(string boardId, string[] newColumnsNames)
        {
            var oldColumns = (await _trelloBoardClient.GetAllListsAsync(boardId)).ToList();
            var newColumns = new Dictionary<string, TrelloList>();
            for (var i = newColumnsNames.Length - 1; i >= 0; i--)
            {
                var newColumn = await _trelloListClient.CreateAsync(newColumnsNames[i], boardId);
                newColumns[newColumn.Name] = newColumn;
            }

            foreach (var oldColumn in oldColumns)
            {
                var cards = await _trelloListClient.GetAllCardsAsync(oldColumn.Id);
                foreach (var card in cards)
                    await _trelloCardClient.ReplaceToListAsync(card.Id, newColumns[oldColumn.Name].Id);
                // if (newColumns.ContainsKey(oldColumn.Name)) 
                await _trelloListClient.ArchiveAsync(oldColumn.Id);
            }
        }

        public async Task<IEnumerable<Column>> GetAllColumnsAsync(string boardId) =>
            (await _trelloBoardClient.GetAllListsAsync(boardId)).Select(t => new Column(t.Id, t.Name, t.Pos));

        public async Task AddMemberAsync(string boardId, string userId) => 
            await _trelloBoardClient.AddMemberAsync(boardId, TrelloMemberTypes.Normal, userId);

        public async Task DeleteBoardAsync(string boardId) => 
            await _trelloBoardClient.DeleteAsync(boardId);
    }
}