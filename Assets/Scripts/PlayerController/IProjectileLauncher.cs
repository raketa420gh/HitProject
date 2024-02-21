using UnityEngine;

public interface IProjectileLauncher
{
    int ProjectilesAmount { get; }
    
    void Initialize(IFactory factory, UIIntValueView projectilesAmountView, int startProjectilesAmount);
    void LaunchForwardNewProjectile(Vector3 mousePosition);
    void AddProjectiles(int amount);
    void RemoveProjectiles(int amount);
}