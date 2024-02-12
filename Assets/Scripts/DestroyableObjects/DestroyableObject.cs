using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private Fracture _fracture;
    private bool _isCollidedBefore;

    private void Awake()
    {
        _isCollidedBefore = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollidedBefore)
            return;
        
        PlayerProjectile playerProjectile = collision.gameObject.GetComponent<PlayerProjectile>();

        if (!playerProjectile)
            return;

        _isCollidedBefore = true;
        
        InvokeCollidedPlayerProjectileEvent();
    }

    protected virtual void InvokeCollidedPlayerProjectileEvent()
    {
        Debug.Log($"{gameObject.name} collided with player projectile");
    }
}