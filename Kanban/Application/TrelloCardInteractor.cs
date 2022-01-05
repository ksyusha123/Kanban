using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public class TrelloCardInteractor : ICardInteractor
    {
        public Task<Card> CreateCardAsync(string name, string boardId)
        {
            throw new System.NotImplementedException();
        }

        public Task EditCardNameAsync(string cardId, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task AssignCardExecutor(string cardId, string executorId)
        {
            throw new System.NotImplementedException();
        }

        public Task ChangeState(string cardId, Column column)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Card>> GetCardsAsync(string nameQuery, string boardId)
        {
            throw new System.NotImplementedException();
        }
    }
}