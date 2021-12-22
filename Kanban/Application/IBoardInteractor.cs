using System.Threading.Tasks;
using Domain;

namespace Application
{
    public interface IBoardInteractor
    {
        Task<Board> CreateBoardAsync(string name);
        Task DeleteCardAsync(string cardId, string boardId);
    }
}