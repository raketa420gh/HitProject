using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    [SerializeField] private ProjectilePool _projectilePool;
    
    public void Initialize()
    {
        _projectilePool.Initialize();
    }

    public ProjectilePhysical CreateProjectilePhysical(Transform parent)
    {
        ProjectilePhysical projectilePhysical = _projectilePool.GetElement();
        projectilePhysical.transform.SetParent(parent);

        return projectilePhysical;
    }
}