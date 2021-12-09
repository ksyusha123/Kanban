using System;
using Domain;

namespace Application
{
    public class CreateCardInteractor
    {
        private readonly IRepository<Card, Guid> _cardRepository;
        public CreateCardInteractor(IRepository<Card, Guid> cardRepository) => _cardRepository = cardRepository;

        public async System.Threading.Tasks.Task CreateTaskAsync(Card card)
        {
            await _cardRepository.AddAsync(card);
        }
    }
}