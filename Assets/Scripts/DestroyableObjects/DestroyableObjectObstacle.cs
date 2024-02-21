using System;
using UnityEngine;

public class DestroyableObjectObstacle : DestroyableObject
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private bool _isAnimated = true;
    
    public event Action OnCollidedPlayer;

    public void Initialize()
    {
        _rigidbody.isKinematic = true;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        PlayerCollider playerCollider = collision.gameObject.GetComponent<PlayerCollider>();
        
        if (!playerCollider)
            return;
        
        Debug.Log($"OnCollidedPlayer");
        
        OnCollidedPlayer?.Invoke();
    }

    protected override void InvokeCollidedPlayerProjectileEvent()
    {
        base.InvokeCollidedPlayerProjectileEvent();

        _rigidbody.isKinematic = false;
    }
}