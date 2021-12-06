using System;
using Domain;

namespace Application
{
    public class CreateProjectInteractor
    {
        private readonly IRepository<Project, Guid> _projectRepository;

        public CreateProjectInteractor(IRepository<Project, Guid> projectRepository) 
            => _projectRepository = projectRepository;

        public async System.Threading.Tasks.Task CreateProjectAsync(Project project)
        {
            await _projectRepository.AddAsync(project);
        }
    }
}