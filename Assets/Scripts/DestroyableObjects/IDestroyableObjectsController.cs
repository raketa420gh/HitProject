using System;

public interface IDestroyableObjectsController
{
    event Action OnObstacleCollidePlayer;
                                                                                
    void Initialize(IFactory factory, IPlayerController playerController, IUIController uiController);
}
