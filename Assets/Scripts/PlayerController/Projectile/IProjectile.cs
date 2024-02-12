using UnityEngine;

public interface IProjectile
{
    void Launch(Vector3 direction, float force);
    void LaunchToLocalForward(float force);
}