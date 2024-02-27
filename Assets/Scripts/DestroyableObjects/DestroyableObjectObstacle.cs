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
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        Debug.Log($"{gameObject.name} triggered {other.gameObject.name}");

        PlayerBehaviour player = other.gameObject.GetComponent<PlayerBehaviour>();
        
        if (!player)
            return;
        
        Debug.Log($"Obstacle triggered Player");
        
        OnCollidedPlayer?.Invoke();
        
        gameObject.SetActive(false);
    }

    protected override void InvokeCollidedPlayerProjectileEvent()
    {
        base.InvokeCollidedPlayerProjectileEvent();

        _rigidbody.isKinematic = false;
    }
}