using UnityEngine;
using Zenject;

public class PowerUpsControllerInstaller : MonoInstaller
{
    [SerializeField] private PowerUpsController _powerUpsController;

    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        Container.Bind<IPowerUpsController>().FromInstance(_powerUpsController).AsSingle().NonLazy();
    }
}