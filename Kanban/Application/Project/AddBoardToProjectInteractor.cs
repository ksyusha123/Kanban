using System;
using Domain;

namespace Application
{
    public class AddBoardToProjectInteractor
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<Board, Guid> _boardRepository;

        public AddBoardToProjectInteractor(IRepository<Project, Guid> projectRepository, IRepository<Board, Guid> tableRepository)
            => (_projectRepository, _boardRepository) = (projectRepository, tableRepository);

        public async System.Threading.Tasks.Task AddTableToProjectAsync(Guid tableId, Guid projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);
            var table = await _boardRepository.GetAsync(tableId);
            
            project.AddTable(table);
            await _projectRepository.UpdateAsync(project);
        }
    }
}