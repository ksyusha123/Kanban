using Domain;

namespace Application
{
    public interface IApplication
    {
        string Name { get; }
        App App { get; }
        IBoardInteractor BoardInteractor { get; }
        ICardInteractor CardInteractor { get; }
    }
}