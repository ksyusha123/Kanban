using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using TrelloApi;

namespace Application
{
    class TrelloBoardInteractor : IBoardInteractor
    {
        private readonly TrelloBoardClient _trelloBoardClient;
        private readonly TrelloCardClient _trelloCardClient;
        
        public TrelloBoardInteractor(TrelloClient trelloClient)
        {
            _trelloCardClient = new TrelloCardClient(trelloClient);
            _trelloBoardClient = new TrelloBoardClient(trelloClient);
        }
        
        public async Task<Board> CreateBoardAsync(string name)
        {
            var trelloBoard = await _trelloBoardClient.CreatedAsync(name);
            var columns = (await _trelloBoardClient.GetAllListsAsync(trelloBoard.Id))
                .Select(t => new Column(t.Name, t.Pos, t.Id))
                .ToList();
            return new Board(trelloBoard.Name, columns, trelloBoard.Id);
        }

        public async Task DeleteCardAsync(string cardId, string boardId) => await _trelloCardClient.DeleteAsync(cardId);

        public async Task<IEnumerable<Column>> GetAllColumnsAsync(string boardId) => 
            (await _trelloBoardClient.GetAllListsAsync(boardId)).Select(t => new Column(t.Name, t.Pos, t.Id));
    }
}