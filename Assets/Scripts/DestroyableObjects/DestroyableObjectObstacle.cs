using System;
using UnityEngine;

public class DestroyableObjectObstacle : DestroyableObject
{
    public event Action OnCollidedPlayer;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        PlayerCollider playerCollider = collision.gameObject.GetComponent<PlayerCollider>();
        
        if (!playerCollider)
            return;
        
        OnCollidedPlayer?.Invoke();
    }
}