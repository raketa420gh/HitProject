public interface IPlayerController
{
    void Initialize(IFactory factory, IUIController uiController);
    void StartMoveForward();
    void StopMove();
    void EnableShooting();
    void DisableShooting();
    void AddProjectiles(int amount);
}