using UnityEngine;

public interface IProjectileLauncher
{
    void Initialize(IFactory factory, UIIntValueView projectilesAmountView, int startProjectilesAmount);
    void LaunchForwardNewProjectile(Vector3 mousePosition);
}