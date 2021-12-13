using System;
using Domain;

namespace Application
{
    public class DeleteBoardInteractor
    {
        private readonly IRepository<Board, Guid> _boardRepository;
        private readonly IRepository<Project, Guid> _projectRepository;

        public DeleteBoardInteractor(IRepository<Board, Guid> tableRepository, IRepository<Project, Guid> projectRepository) => 
            (_boardRepository, _projectRepository) = (tableRepository, projectRepository);

        public async System.Threading.Tasks.Task DeleteTableAsync(Guid tableId, Guid projectId)
        {
            var table = await _boardRepository.GetAsync(tableId);
            var project = await _projectRepository.GetAsync(projectId);
            
            project.RemoveBoard(table);
            await _boardRepository.DeleteAsync(table);
            await _projectRepository.UpdateAsync(project);
        }
    }
}