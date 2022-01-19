using Application.OwnKanban;
using Domain;
using Infrastructure;
using Moq;
using NUnit.Framework;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Domain.Chat;

namespace Tests
{
    public abstract class CommandTests
    {
        protected TestRepository<Board> BoardRepository;
        protected TestRepository<Card> CardRepository;
        protected TestRepository<Executor> ExecutorRepository;
        protected TestRepository<Column> ColumnRepository;
        protected OwnKanbanApplication[] Apps;
        protected Mock<TelegramBotClient> Client;
        protected Mock<Message> Message;
        protected string _boardId;
        protected Chat _chat;

        [SetUp]
        public virtual void Init()
        {
            BoardRepository = new TestRepository<Board>();
            CardRepository = new TestRepository<Card>();
            ExecutorRepository = new TestRepository<Executor>();
            ColumnRepository = new TestRepository<Column>();
            Apps = new[]
            {
                new OwnKanbanApplication(BoardRepository, CardRepository, ExecutorRepository, ColumnRepository,
                    new StandardDateTimeProvider())
            };
            Client = new Mock<TelegramBotClient>();
            Message = new Mock<Message>();

            _boardId = "1";
            _chat = new Chat("testChat", App.OwnKanban, _boardId);
        }
    }
}