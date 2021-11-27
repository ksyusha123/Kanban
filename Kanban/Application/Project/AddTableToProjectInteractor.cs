using System;
using Domain;

namespace Application
{
    public class AddTableToProjectInteractor
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Table> _tableRepository;

        public AddTableToProjectInteractor(IRepository<Project> projectRepository, IRepository<Table> tableRepository)
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