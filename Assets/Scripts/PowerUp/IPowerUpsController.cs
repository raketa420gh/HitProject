public interface IPowerUpsController
{
    PowerUp[] PowerUps { get; }
    
    void Initialise(ISaveService saveService);
}