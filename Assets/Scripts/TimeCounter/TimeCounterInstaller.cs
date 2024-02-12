using UnityEngine;
using Zenject;

public class TimeCounterInstaller : MonoInstaller
{
    [SerializeField] private TimeCounter _timeCounter;

    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        Container.Bind<TimeCounter>().FromInstance(_timeCounter).AsSingle().NonLazy();
    }
}