using UnityEngine;

public class SoloGameState : GameLoopState
{
    public SoloGameState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
    }

    public override void OnStateDisabled()
    {
        
    }

    public override void Update()
    {
        
    }
}