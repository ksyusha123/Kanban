using System.Threading.Tasks;
using Domain;

namespace Application
{
    public interface ICardInteractor
    {
        Task CreateCardAsync(string name);
        Task EditCardNameAsync(string cardId, string name);
        Task AssignCardExecutor(string cardId, string executorId);
        Task ChangeState(string cardId, State state);
    }
}