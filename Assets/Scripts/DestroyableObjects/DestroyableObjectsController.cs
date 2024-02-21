using System;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjectsController : MonoBehaviour, IDestroyableObjectsController
{
    [SerializeField] private List<DestroyableObjectProjectilesAdder> _destroyableProjectilesAdders;
    [SerializeField] private List<DestroyableObjectObstacle> _destroyableObstacles;
    private IFactory _factory;
    private IPlayerController _playerController;
    private IUIController _uiController;

    public event Action OnObstacleCollidePlayer;

    private void OnEnable()
    {
        foreach (DestroyableObjectProjectilesAdder destroyableProjectilesAdder in _destroyableProjectilesAdders)
            destroyableProjectilesAdder.OnDestroyProjectileAdder += HandleDestroyProjectileAddersEvent;

        foreach (DestroyableObjectObstacle destroyableObstacle in _destroyableObstacles)
            destroyableObstacle.OnCollidedPlayer += HandleObstacleCollidedPlayerEvent;
    }

    private void OnDisable()
    {
        foreach (DestroyableObjectProjectilesAdder destroyableProjectilesAdder in _destroyableProjectilesAdders)
            destroyableProjectilesAdder.OnDestroyProjectileAdder -= HandleDestroyProjectileAddersEvent;
        
        foreach (DestroyableObjectObstacle destroyableObstacle in _destroyableObstacles)
            destroyableObstacle.OnCollidedPlayer -= HandleObstacleCollidedPlayerEvent;
    }

    public void Initialize(IFactory factory, IPlayerController playerController, IUIController uiController)
    {
        _factory = factory;
        _playerController = playerController;
        _uiController = uiController;
        
        foreach (DestroyableObjectObstacle destroyableObstacle in _destroyableObstacles)
            destroyableObstacle.Initialize();
    }

    private void HandleDestroyProjectileAddersEvent(Vector3 position, int projectilesAddAmount)
    {
        _playerController.AddProjectiles(projectilesAddAmount);

        UIFloatingText floatingText = _factory.UI.CreateFloatingText(_uiController.HudPanel.transform);
        floatingText.Initialize(_uiController.CanvasRect);
        floatingText.ActivateFloatingText(position, projectilesAddAmount);
    }

    private void HandleObstacleCollidedPlayerEvent()
    {
        OnObstacleCollidePlayer?.Invoke();
    }
}