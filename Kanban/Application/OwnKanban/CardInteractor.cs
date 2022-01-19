using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Infrastructure;

namespace Application.OwnKanban
{
    public class CardInteractor : ICardInteractor
    {
        private readonly IRepository<Card> _cardRepository;
        private readonly IRepository<Executor> _executorRepository;
        private readonly IRepository<Board> _boardRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CardInteractor(IRepository<Card> cardRepository, IRepository<Executor> executorRepository,
            IRepository<Board> boardRepository, IDateTimeProvider dateTimeProvider)
        {
            _cardRepository = cardRepository;
            _executorRepository = executorRepository;
            _boardRepository = boardRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Card> CreateCardAsync(string name, string boardId)
        {
            var board = await _boardRepository.GetAsync(boardId);
            var card = new Card(name, "", board.StartColumn.Id, _dateTimeProvider);
            board.AddCard(card);
            await _boardRepository.UpdateAsync(board);
            return card;
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(IEnumerable<string> nameTokens, string boardId)
        {
            var board = await _boardRepository.GetAsync(boardId);
            return board.Cards.Where(c => nameTokens.Any(t => c.Name.Contains(t, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task<Card> GetCard(string name, string boardId) =>
            (await _boardRepository.GetAsync(boardId)).Cards
            .FirstOrDefault(c => c.Name == name);
        

        public async Task EditCardNameAsync(string cardId, string name)
        {
            var card = await _cardRepository.GetAsync(cardId);
            if (name != null) card.Name = name;
            await _cardRepository.UpdateAsync(card);
        }

        public async Task AssignCardExecutorAsync(string cardId, string executorId)
        {
            var executor = await _executorRepository.GetAsync(executorId);
            var card = await _cardRepository.GetAsync(cardId);
            card.Executor = executor;
            await _cardRepository.UpdateAsync(card);
        }

        public async Task ChangeColumnAsync(string cardId, Column column)
        {
            var card = await _cardRepository.GetAsync(cardId);
            card.ColumnId = column.Id;
            await _cardRepository.UpdateAsync(card);
        }

        public async Task AddComment(string cardId, string comment, string executorId)
        {
            var card = await _cardRepository.GetAsync(cardId);
            var executor = await _executorRepository.GetAsync(executorId);
            card.AddComment(new Comment(executor, comment, _dateTimeProvider));
            await _cardRepository.UpdateAsync(card);
        }
    }
}