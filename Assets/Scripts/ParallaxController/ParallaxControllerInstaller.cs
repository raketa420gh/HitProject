using UnityEngine;
using Zenject;

public class ParallaxControllerInstaller : MonoInstaller
{
    [SerializeField] private ParallaxController _parallaxController;
    
    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        Container.Bind<ParallaxController>().FromInstance(_parallaxController).AsSingle().NonLazy();
    }
}