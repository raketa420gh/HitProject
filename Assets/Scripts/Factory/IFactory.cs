public interface IFactory
{
    UIFactory UI { get; }
    ProjectileFactory ProjectileFactory { get; }

    void Initialize();
}