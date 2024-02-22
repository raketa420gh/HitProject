using System;
using UnityEngine;

public interface IProjectileLauncher
{
    int ProjectilesAmount { get; }
    
    event Action OnProjectilesAmountChanged;
    event Action OnProjectilesEnd;
    
    void Initialize(IFactory factory, UIIntValueView projectilesAmountView, int startProjectilesAmount);
    void LaunchForwardNewProjectile(Vector3 mousePosition);
    void AddProjectiles(int amount);
    void RemoveProjectiles(int amount);
}