using UnityEngine;
using Zenject;

public class UIControllerInstaller : MonoInstaller
{
    [SerializeField] private UIController _uiController;

    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        Container.Bind<IUIController>().FromInstance(_uiController).AsSingle().NonLazy();
    }
}