using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloApi;

namespace Application
{
    class TrelloBoardInteractor: IBoardInteractor
    {
        public async Task CreateBoardAsync(string name)
        {
            await TrelloBoard.CreateBoardAsync(name);
        }

        public async Task AddCardAsync(string cardId, string boardId)
        {
            var board = new TrelloBoard(boardId);
            var someList = board.GetAllLists().FirstOrDefault();
            if (someList is null)
                throw new NullReferenceException("Board hasn't any list");
            var card = new TrelloCard(cardId);
            await card.ReplaceToListAsync(someList.Id);
        }

        public async Task DeleteCardAsync(string cardId, string boardId)
        {
            var card = new TrelloCard(cardId);
            await card.DeleteAsync();
        }
    }
}
