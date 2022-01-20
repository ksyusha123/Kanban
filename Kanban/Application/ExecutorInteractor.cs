using System.Threading.Tasks;
using Domain;

namespace Application
{
    public class ExecutorInteractor
    {
        private readonly IRepository<Executor> _executorRepository;

        public ExecutorInteractor(IRepository<Executor> executorRepository) => 
            _executorRepository = executorRepository;

        public async Task AddExecutorAsync(string appUsername, string telegramUsername) => 
            await _executorRepository.AddAsync(new Executor(telegramUsername, appUsername));
        
        public async Task AddExecutorAsync(string telegramUsername) => 
            await _executorRepository.AddAsync(new Executor(telegramUsername));

        public async Task<Executor> GetExecutor(string telegramId) => 
            await _executorRepository.GetAsync(telegramId);
    }
}