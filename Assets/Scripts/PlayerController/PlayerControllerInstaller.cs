using UnityEngine;
using Zenject;

public class PlayerControllerInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;

    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        Container.Bind<IPlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
    }
}