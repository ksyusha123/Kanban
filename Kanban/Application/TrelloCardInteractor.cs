using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly TrelloListClient _trelloListClient;
        private readonly IDateTimeProvider _dateTimeProvider;

        public TrelloCardInteractor(TrelloClient trelloClient, IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _trelloBoardClient = new TrelloBoardClient(trelloClient);
            _trelloCardClient = new TrelloCardClient(trelloClient);
            _trelloListClient = new TrelloListClient(trelloClient);
        }

        public async Task<Card> CreateCardAsync(string name, string boardId)
        {
            var startColumn = (await _trelloBoardClient.GetAllListsAsync(boardId))
                .OrderBy(t => t.Pos)
                .First();
            var card = await _trelloCardClient.Create(startColumn.Id, name);
            return new Card(card.Id, name, "", new Executor("", ""), startColumn.Id, _dateTimeProvider);
        }

        public async Task EditCardNameAsync(string cardId, string name) => await _trelloCardClient.Rename(cardId, name);

        public async Task AssignCardExecutorAsync(string cardId, string executorId)
        {
            throw new System.NotImplementedException();
        }

        public async Task ChangeColumnAsync(string cardId, Column column) =>
            await _trelloCardClient.ReplaceToListAsync(cardId, column.Id);

        public async Task<IEnumerable<Card>> GetCardsAsync(IEnumerable<string> nameTokens, string boardId)
        {
            var columns = await _trelloBoardClient.GetAllListsAsync(boardId);
            var cards = Enumerable.Empty<Card>();
            foreach (var column in columns)
            {
                var trelloCards = (await _trelloListClient.GetAllCardsAsync(column.Id))
                    .Select(c => new Card(c.Id, c.Name, c.Desc, new Executor("", ""), column.Id, _dateTimeProvider));
                cards = cards.Concat(trelloCards);
            }
            return cards
                .Where(c => nameTokens.Any(t => c.Name.Contains(t, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        public async Task<Card> GetCard(string name, string boardId)
        {
            var columns = await _trelloBoardClient.GetAllListsAsync(boardId);
            foreach (var column in columns)
            foreach (var card in await _trelloListClient.GetAllCardsAsync(column.Id))
                if (card.Name == name)
                    return new Card(card.Id, card.Name, card.Desc, 
                        new Executor("", ""), column.Id, _dateTimeProvider);

            return null;
        }
    }
}