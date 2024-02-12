using System;
using UnityEngine;

public class DestroyableObjectProjectilesAdder : DestroyableObject
{
    [SerializeField] private int _projectilesAddAmount = 3;

    public event Action<Vector3, int> OnDestroyProjectileAdder;

    protected override void InvokeCollidedPlayerProjectileEvent()
    {
        base.InvokeCollidedPlayerProjectileEvent();
        
        OnDestroyProjectileAdder?.Invoke(transform.position, _projectilesAddAmount);
    }
}