using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public interface ICardInteractor
    {
        Task<Card> CreateCardAsync(string name, string boardId);
        Task EditCardNameAsync(string cardId, string name);
        Task AssignCardExecutorAsync(string cardId, string executorId);
        Task ChangeColumnAsync(string cardId, Column column);
        Task<IEnumerable<Card>> GetCardsAsync(IEnumerable<string> nameTokens, string boardId);
        Task<Card> GetCard(string name, string boardId);
        Task AddComment(string cardId, string comment, string executorId);
    }
}