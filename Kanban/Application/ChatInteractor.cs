using System.Threading.Tasks;
using Domain;

namespace Application
{
    public class ChatInteractor
    {
        private readonly IRepository<Chat, long> _chatRepository;

        public ChatInteractor(IRepository<Chat, long> chatRepository) => _chatRepository = chatRepository;

        public async Task<Chat> GetChatAsync(long chatId) => await _chatRepository.GetAsync(chatId);

        public async Task AddChatAsync(long chatId, App app, string boardId)
        {
            var chat = new Chat(chatId, app, boardId);
            await _chatRepository.AddAsync(chat);
        }
    }
}