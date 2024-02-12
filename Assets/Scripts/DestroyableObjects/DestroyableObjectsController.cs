using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjectsController : MonoBehaviour, IDestroyableObjectsController
{
    [SerializeField] private List<DestroyableObjectProjectilesAdder> _destroyableProjectilesAdders;
    private IPlayerController _playerController;

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

    public void Initialize(IPlayerController playerController)
    {
        _playerController = playerController;
    }

    private void HandleDestroyProjectileAddersEvent(int projectilesAddAmount)
    {
        _playerController.AddProjectiles(projectilesAddAmount);
    }
}