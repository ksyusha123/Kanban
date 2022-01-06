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
        public async Task<Board> CreateBoardAsync(string name)
        {
            var trelloBoard = await TrelloBoard.CreateBoardAsync(name);
            var columns = trelloBoard
                .GetAllLists()
                .Select(t => new Column(t.Name, t.Pos))
                .ToList();
            return new Board(trelloBoard.Name, columns);
        }

        public async Task DeleteCardAsync(string cardId, string boardId)
        {
            var card = new TrelloCard(cardId);
            await card.DeleteAsync();
        }
    }
}