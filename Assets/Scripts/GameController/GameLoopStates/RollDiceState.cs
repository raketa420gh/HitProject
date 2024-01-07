using UnityEngine;

public class RollDiceState : GameLoopState
{
    public RollDiceState(GameLoopStateMachine stateMachine) : base(stateMachine)
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