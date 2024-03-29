using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private Fracture _fracture;
    private bool _isCollidedBefore;

    private void Awake()
    {
        _isCollidedBefore = false;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (_isCollidedBefore)
            return;
        
        PlayerProjectile playerProjectile = collision.gameObject.GetComponent<PlayerProjectile>();

        if (!playerProjectile)
            return;

        _isCollidedBefore = true;
        
        InvokeCollidedPlayerProjectileEvent();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
    }

    protected virtual void InvokeCollidedPlayerProjectileEvent()
    {
    }
}