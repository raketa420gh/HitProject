using System;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour, IProjectileLauncher
{
    [SerializeField] private float _launchForce = 1f;
    [SerializeField] private float _spawnPositionOffsetForward = 0.3f;
    [SerializeField] private float _spawnPositionOffsetUp = -0.25f;
    private UIIntValueView _projectilesAmountView;
    private int _projectilesAmount;
    private Camera _camera;
    private IFactory _factory;

    public event Action OnProjectilesAmountChanged;
    public event Action OnProjectilesOut;
    
    public void Initialize(IFactory factory, UIIntValueView projectilesAmountView, int startProjectilesAmount)
    {
        _factory = factory;
        _camera = Camera.main;
        _projectilesAmountView = projectilesAmountView;
        
        SetProjectilesAmount(startProjectilesAmount);
    }

    public void LaunchForwardNewProjectile(Vector3 mousePosition)
    {
        if (!CheckProjectileLaunchCapability())
            return;
        
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 spawnPosition = hit.point; 
            ProjectilePhysical projectile = _factory.ProjectileFactory.CreateProjectilePhysical(null);
            projectile.transform.position = _camera.ScreenToWorldPoint(mousePosition) + 
                                            Vector3.forward * _spawnPositionOffsetForward + Vector3.up * _spawnPositionOffsetUp;
            projectile.transform.LookAt(hit.point);
            projectile.LaunchToLocalForward(_launchForce);
        }

        _projectilesAmount--;
        
        _projectilesAmountView.SetValueView(_projectilesAmount);
        OnProjectilesAmountChanged?.Invoke();
        
        if (_projectilesAmount == 0)
            OnProjectilesOut?.Invoke();
    }

    public void AddProjectiles(int amount)
    {
        _projectilesAmount += amount;
        
        _projectilesAmountView.SetValueView(_projectilesAmount);
        OnProjectilesAmountChanged?.Invoke();
    }

    private void SetProjectilesAmount(int amount)
    {
        _projectilesAmount = amount;
        
        _projectilesAmountView.SetValueView(_projectilesAmount);
        OnProjectilesAmountChanged?.Invoke();
    }

    private bool CheckProjectileLaunchCapability()
    {
        return _projectilesAmount > 0;
    }
}