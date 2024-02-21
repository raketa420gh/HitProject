using System;

public interface IPlayerController
{
    event Action OnProjectilesEnd;
    
    void Initialize(IFactory factory, IUIController uiController);
    void StartMoveForward();
    void StopMove();
    void EnableShooting();
    void DisableShooting();
    void AddProjectiles(int amount);
    void RemoveProjectiles(int amount);
}