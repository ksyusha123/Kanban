using System.Threading.Tasks;
using Domain;

namespace Application
{
    public interface IBoardInteractor
    {
        Task<Board> CreateBoardAsync(string name);
        Task AddCardAsync(string cardId, string boardId);
        Task DeleteCardAsync(string cardId, string boardId);
    }
}