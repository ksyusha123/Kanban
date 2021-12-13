using System;
using Domain;

namespace Application
{
    public class ChangeCardStateInteractor
    {
        private readonly IRepository<Card, Guid> _cardRepository;

        public ChangeCardStateInteractor(IRepository<Card, Guid> cardRepository) => _cardRepository = cardRepository;

        public async System.Threading.Tasks.Task ChangeCardState(Guid cardId, State state)
        {
            var card = await _cardRepository.GetAsync(cardId);
            card.State = state;
            
            await _cardRepository.UpdateAsync(card);
        }
    }
}