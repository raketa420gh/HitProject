using UnityEngine;
using Zenject;

public class DestroyableObjectsControllerInstaller : MonoInstaller
{
    [SerializeField] private DestroyableObjectsController _destroyableObjectsController;

    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        Container.Bind<IDestroyableObjectsController>().FromInstance(_destroyableObjectsController).AsSingle().NonLazy();
    }
}