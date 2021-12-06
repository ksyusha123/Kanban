using System;
using Domain;

namespace Application
{
    public class DeleteTableInteractor
    {
        private readonly IRepository<Table, Guid> _tableRepository;
        private readonly IRepository<Project, Guid> _projectRepository;

        public DeleteTableInteractor(IRepository<Table, Guid> tableRepository, IRepository<Project, Guid> projectRepository) => 
            (_tableRepository, _projectRepository) = (tableRepository, projectRepository);

        public async System.Threading.Tasks.Task DeleteTableAsync(Guid tableId, Guid projectId)
        {
            var table = await _tableRepository.GetAsync(tableId);
            var project = await _projectRepository.GetAsync(projectId);
            
            project.RemoveTable(table);
            await _tableRepository.DeleteAsync(table);
            await _projectRepository.UpdateAsync(project);
        }
    }
}