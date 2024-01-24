using UnityEngine;
using Zenject;

public class DicePhysicalInstaller : MonoInstaller
{
    [SerializeField] private DicePhysical _dicePhysical;

    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        Container.Bind<DicePhysical>().FromInstance(_dicePhysical).AsSingle().NonLazy();
    }
}