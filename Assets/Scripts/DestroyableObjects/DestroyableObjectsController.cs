using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjectsController : MonoBehaviour, IDestroyableObjectsController
{
    [SerializeField] private List<DestroyableObjectProjectilesAdder> _destroyableProjectilesAdders;
    private IFactory _factory;
    private IPlayerController _playerController;
    private IUIController _uiController;

    private void OnEnable()
    {
        for (int i = 0; i < _destroyableProjectilesAdders.Count; i++)
        {
            _destroyableProjectilesAdders[i].OnDestroyProjectileAdder += HandleDestroyProjectileAddersEvent;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _destroyableProjectilesAdders.Count; i++)
        {
            _destroyableProjectilesAdders[i].OnDestroyProjectileAdder -= HandleDestroyProjectileAddersEvent;
        }
    }

    public void Initialize(IFactory factory, IPlayerController playerController, IUIController uiController)
    {
        _factory = factory;
        _playerController = playerController;
        _uiController = uiController;
    }

    private void HandleDestroyProjectileAddersEvent(Vector3 position, int projectilesAddAmount)
    {
        _playerController.AddProjectiles(projectilesAddAmount);

        UIFloatingText floatingText = _factory.UI.CreateFloatingText(_uiController.HudPanel.transform);
        floatingText.Initialize(_uiController.CanvasRect);
        floatingText.ActivateFloatingText(position, projectilesAddAmount);
    }
}