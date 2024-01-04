public interface IFactory
{
    UIFactory UI { get; }

    void Initialize();
}