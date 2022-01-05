using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Infrastructure;

namespace Application
{
    public class CardInteractor : ICardInteractor
    {
        private readonly IRepository<Card, Guid> _cardRepository;
        private readonly IRepository<Executor, Guid> _executorRepository;
        private readonly IRepository<Board, Guid> _boardRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CardInteractor(IRepository<Card, Guid> cardRepository, IRepository<Executor, Guid> executorRepository,
            IRepository<Board, Guid> boardRepository, IDateTimeProvider dateTimeProvider)
        {
            _cardRepository = cardRepository;
            _executorRepository = executorRepository;
            _boardRepository = boardRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Card> CreateCardAsync(string name, string boardId)
        {
            var board = await _boardRepository.GetAsync(new Guid(boardId));
            var card = new Card(name, "", new Executor("", ""), board.StartColumn, _dateTimeProvider);
            board.AddCard(card);
            await _boardRepository.UpdateAsync(board);
            return card;
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(string nameQuery, string boardId)
        {
            var board = await _boardRepository.GetAsync(new Guid(boardId));
            var nameTokens = nameQuery.Split(' ');
            return board.Cards.Where(c => nameTokens.Any(t => c.Name.Contains(t, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task EditCardNameAsync(string cardId, string name)
        {
            var card = await _cardRepository.GetAsync(new Guid(cardId));
            if (name != null) card.Name = name;
            await _cardRepository.UpdateAsync(card);
        }

        public async Task AssignCardExecutor(string cardId, string executorId)
        {
            var executor = await _executorRepository.GetAsync(new Guid(executorId));
            var task = await _cardRepository.GetAsync(new Guid(cardId));
            task.Executor = executor;
            await _cardRepository.UpdateAsync(task);
        }

        public async Task ChangeState(string cardId, Column column)
        {
            var card = await _cardRepository.GetAsync(new Guid(cardId));
            card.Column = column;
            await _cardRepository.UpdateAsync(card);
        }

        public async Task AddComment(string cardId, Comment comment)
        {
        }
    }
}