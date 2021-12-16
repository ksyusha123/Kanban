using System;
using System.Threading.Tasks;
using Domain;
using Infrastructure;

namespace Application
{
    public class CardInteractor : ICardInteractor
    {
        private readonly IRepository<Card, Guid> _cardRepository;
        private readonly IRepository<Executor, Guid> _executorRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CardInteractor(IRepository<Card, Guid> cardRepository, IRepository<Executor, Guid> executorRepository, 
            IDateTimeProvider dateTimeProvider) =>
            (_cardRepository, _executorRepository, _dateTimeProvider) = (cardRepository, executorRepository, dateTimeProvider);

        public async Task CreateCardAsync(string name)
        {
            var card = new Card(name, _dateTimeProvider);
            await _cardRepository.AddAsync(card);
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

        public async Task ChangeState(string cardId, State state)
        {
            var card = await _cardRepository.GetAsync(new Guid(cardId));
            card.State = state;
            await _cardRepository.UpdateAsync(card);
        }
    }
}