using Domain;
using Infrastructure;
using TrelloApi;

namespace Application.Trello
{
    class TrelloApplication : IApplication
    {
        public TrelloApplication(TrelloClient trelloClient, IDateTimeProvider dateTimeProvider)
        {
            BoardInteractor = new TrelloBoardInteractor(trelloClient, dateTimeProvider);
            CardInteractor = new TrelloCardInteractor(trelloClient, dateTimeProvider);
        }
        public string Name => nameof(App.Trello);
        public App App => App.Trello;
        public IBoardInteractor BoardInteractor { get; }
        public ICardInteractor CardInteractor { get; }
    }
}