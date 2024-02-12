using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private int _poolCount = 25;
    [SerializeField] private bool _isAutoExpand = true;
    [SerializeField] private ProjectilePhysical _prefab;

    private Pool<ProjectilePhysical> _pool;

    public void Initialize()
    {
        _pool = new Pool<ProjectilePhysical>(_prefab, _poolCount, transform)
        {
            AutoExpand = _isAutoExpand
        };
    }

    public ProjectilePhysical GetElement()
    {
        return _pool.GetFreeElement();
    }
}