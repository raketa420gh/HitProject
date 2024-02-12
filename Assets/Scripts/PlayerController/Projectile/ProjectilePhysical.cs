using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectilePhysical : MonoBehaviour, IProjectile
{
    [SerializeField] private float _deactivateDelayTime = 10f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction, float force)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction * force);

        DisableAsync();
    }

    public void LaunchToLocalForward(float force)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.forward * force);

        DisableAsync();
    }
    
    private async UniTaskVoid DisableAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_deactivateDelayTime));
        
        gameObject.SetActive(false);
    }
}