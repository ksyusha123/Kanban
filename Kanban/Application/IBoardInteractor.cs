using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public interface IBoardInteractor
    {
        Task<Board> CreateBoardAsync(string name);
        Task<Board> GetBoardAsync(string boardId);
        Task DeleteCardAsync(string cardId, string boardId);
        Task ChangeColumnsAsync(string boardId, string[] newColumnsNames);
        Task<IEnumerable<Column>> GetAllColumnsAsync(string boardId);
    }
}