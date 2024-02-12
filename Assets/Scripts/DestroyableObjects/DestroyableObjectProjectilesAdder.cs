using System;
using UnityEngine;

public class DestroyableObjectProjectilesAdder : DestroyableObject
{
    [SerializeField] private int _projectilesAddAmount = 3;

    public event Action<int> OnDestroyProjectileAdder;

    protected override void InvokeCollidedPlayerProjectileEvent()
    {
        base.InvokeCollidedPlayerProjectileEvent();
        
        OnDestroyProjectileAdder?.Invoke(_projectilesAddAmount);
    }
}