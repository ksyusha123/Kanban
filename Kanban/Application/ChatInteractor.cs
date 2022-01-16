using System.Threading.Tasks;
using Domain;

namespace Application
{
    public class ChatInteractor
    {
        private readonly IRepository<Chat> _chatRepository;

        public ChatInteractor(IRepository<Chat> chatRepository) => _chatRepository = chatRepository;

        public async Task<Chat> GetChatAsync(string chatId) => await _chatRepository.GetAsync(chatId);

        public async Task AddChatAsync(string chatId, App app, string boardId)
        {
            var chat = new Chat(chatId, app, boardId);
            await _chatRepository.AddAsync(chat);
        }

        public async Task DeleteChatAsync(string chatId)
        {
            var chat = await _chatRepository.GetAsync(chatId);
            await _chatRepository.DeleteAsync(chat);
        }
    }
}