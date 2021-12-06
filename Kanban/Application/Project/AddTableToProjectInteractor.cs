using System;
using Domain;

namespace Application
{
    public class AddTableToProjectInteractor
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<Board, Guid> _tableRepository;

        public AddTableToProjectInteractor(IRepository<Project, Guid> projectRepository, IRepository<Board, Guid> tableRepository)
            => (_projectRepository, _tableRepository) = (projectRepository, tableRepository);

        public async System.Threading.Tasks.Task AddTableToProjectAsync(Guid tableId, Guid projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);
            var table = await _tableRepository.GetAsync(tableId);
            
            project.AddTable(table);
            await _projectRepository.UpdateAsync(project);
        }
    }
}