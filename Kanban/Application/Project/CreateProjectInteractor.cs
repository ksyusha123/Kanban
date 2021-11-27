using Domain;

namespace Application
{
    public class CreateProjectInteractor
    {
        private readonly IRepository<Project> _projectRepository;

        public CreateProjectInteractor(IRepository<Project> projectRepository) 
            => _projectRepository = projectRepository;

        public async System.Threading.Tasks.Task CreateProjectAsync(Project project)
        {
            await _projectRepository.AddAsync(project);
        }
    }
}