using UnityEngine;

public class Factory : MonoBehaviour, IFactory
{
    [SerializeField] private UIFactory _uiFactory;
    [SerializeField] private ProjectileFactory _projectileFactory;

    public UIFactory UI => _uiFactory;
    public ProjectileFactory ProjectileFactory => _projectileFactory;

    public void Initialize()
    {
        _uiFactory.Initialize();
        _projectileFactory.Initialize();
    }
}