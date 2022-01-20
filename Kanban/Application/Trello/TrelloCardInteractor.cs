using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Infrastructure;
using TrelloApi;

namespace Application.Trello
{
    public class TrelloCardInteractor : ICardInteractor
    {
        private readonly TrelloCardClient _trelloCardClient;
        private readonly TrelloBoardClient _trelloBoardClient;
        private readonly TrelloListClient _trelloListClient;
        private readonly TrelloMemberClient _trelloMemberClient;
        private readonly IDateTimeProvider _dateTimeProvider;

        public TrelloCardInteractor(TrelloClient trelloClient, IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _trelloMemberClient = new TrelloMemberClient(trelloClient);
            _trelloBoardClient = new TrelloBoardClient(trelloClient);
            _trelloCardClient = new TrelloCardClient(trelloClient);
            _trelloListClient = new TrelloListClient(trelloClient);
        }

        public async Task<Card> CreateCardAsync(string name, string boardId)
        {
            var startColumn = (await _trelloBoardClient.GetAllListsAsync(boardId))
                .OrderBy(t => t.Pos)
                .First();
            var card = await _trelloCardClient.CreateAsync(startColumn.Id, name);
            return new Card(card.Id, name, "", startColumn.Id, _dateTimeProvider);
        }

        public async Task EditCardNameAsync(string cardId, string name) => 
            await _trelloCardClient.RenameAsync(cardId, name);

        public async Task AssignCardExecutorAsync(string cardId, string executorId) => 
            await _trelloCardClient.AddMemberAsync(cardId, executorId);

        public async Task ChangeColumnAsync(string cardId, Column column) =>
            await _trelloCardClient.ReplaceToListAsync(cardId, column.Id);

        public async Task<IEnumerable<Card>> GetCardsAsync(IEnumerable<string> nameTokens, string boardId)
        {
            var columns = await _trelloBoardClient.GetAllListsAsync(boardId);
            var cards = Enumerable.Empty<Card>();
            foreach (var column in columns)
            {
                var trelloCards = (await _trelloListClient.GetAllCardsAsync(column.Id))
                    .Select(c => new Card(c.Id, c.Name, c.Desc, column.Id, _dateTimeProvider));
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
                    return new Card(card.Id, card.Name, card.Desc, column.Id, _dateTimeProvider);

            return null;
        }

        public async Task AddComment(string cardId, string comment, string authorId) =>
            await _trelloCardClient.AddCommentAsync(cardId, $"{comment}. Автор: {authorId}");

        public async Task<IEnumerable<Comment>> GetComments(string cardId)
        {
            var trelloComments = await _trelloCardClient.GetAllCommentsAsync(cardId);
            var comments = new List<Comment>();
            foreach (var trelloComment in trelloComments)
            {
                var author = (await _trelloMemberClient.LoadAsync(trelloComment.IdMemberCreator)).Username;
                comments.Add(new Comment(new Executor(author), trelloComment.Data.Text, _dateTimeProvider));
            }

            return comments;
        }
    }
}