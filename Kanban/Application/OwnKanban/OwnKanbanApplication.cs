using Domain;
using Infrastructure;

namespace Application.OwnKanban
{
    public class OwnKanbanApplication : IApplication
    {
        public OwnKanbanApplication(IRepository<Board> boardRepository, IRepository<Card> cardRepository,
            IRepository<Executor> executorRepository, IRepository<Column> columnRepository,
            IDateTimeProvider dateTimeProvider)
        {
            BoardInteractor = new BoardInteractor(boardRepository, cardRepository, columnRepository);
            CardInteractor = new CardInteractor(cardRepository, executorRepository, boardRepository, dateTimeProvider);
        }

        public string Name => nameof(App.OwnKanban);
        public App App => App.OwnKanban;
        public IBoardInteractor BoardInteractor { get; }
        public ICardInteractor CardInteractor { get; }
    }
}