using UnityEngine;

public class Factory : MonoBehaviour, IFactory
{
    [SerializeField] private UIFactory _uiFactory;

    public UIFactory UI => _uiFactory;

    public void Initialize()
    {
        _uiFactory.Initialize();
    }
}