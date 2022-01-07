using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Infrastructure;
using TrelloApi;

namespace Application
{
    public class TrelloCardInteractor : ICardInteractor
    {
        private readonly TrelloCardClient _trelloCardClient;
        private readonly TrelloBoardClient _trelloBoardClient;
        private readonly IDateTimeProvider _dateTimeProvider;
        public TrelloCardInteractor(TrelloClient trelloClient, IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _trelloBoardClient = new TrelloBoardClient(trelloClient);
            _trelloCardClient = new TrelloCardClient(trelloClient);
        }
        public Task<Card> CreateCardAsync(string name, string boardId)
        {
            throw new NotImplementedException();
        }

        public Task EditCardNameAsync(string cardId, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task AssignCardExecutor(string cardId, string executorId)
        {
            throw new System.NotImplementedException();
        }

        public Task ChangeColumn(string cardId, Column column)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Card>> GetCardsAsync(string nameQuery, string boardId)
        {
            throw new System.NotImplementedException();
        }
    }
}