using System.Threading.Tasks;

namespace Application
{
    public interface IBoardInteractor
    {
        Task CreateBoardAsync(string name);
        Task AddCardAsync(string cardId, string boardId);
        Task DeleteCardAsync(string cardId, string boardId);
    }
}