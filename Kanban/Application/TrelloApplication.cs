using Domain;
using Infrastructure;
using TrelloApi;

namespace Application
{
    class TrelloApplication : IApplication
    {
        public TrelloApplication(TrelloClient trelloClient, IDateTimeProvider dateTimeProvider)
        {
            BoardInteractor = new TrelloBoardInteractor(trelloClient);
            CardInteractor = new TrelloCardInteractor(trelloClient, dateTimeProvider);
        }
        public string Name => "Trello";
        public App App => App.Trello;
        public IBoardInteractor BoardInteractor { get; }
        public ICardInteractor CardInteractor { get; }
    }
}