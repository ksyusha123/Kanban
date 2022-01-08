using System;
using Domain;
using Infrastructure;

namespace Application
{
    public class OwnKanbanApplication : IApplication
    {
        public OwnKanbanApplication(IRepository<Board, string> boardRepository, IRepository<Card, string> cardRepository,
            IRepository<Executor, Guid> executorRepository, IDateTimeProvider dateTimeProvider)
        {
            BoardInteractor = new BoardInteractor(boardRepository, cardRepository);
            CardInteractor = new CardInteractor(cardRepository, executorRepository, boardRepository, dateTimeProvider);
        }

        public string Name => "OwnKanban";
        public App App => App.OwnKanban;
        public IBoardInteractor BoardInteractor { get; }
        public ICardInteractor CardInteractor { get; }
    }
}