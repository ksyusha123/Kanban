namespace Application
{
    public interface IApplication
    {
        string Name { get; }
        IBoardInteractor BoardInteractor { get; }
        ICardInteractor CardInteractor { get; }
    }
}