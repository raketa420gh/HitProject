using UnityEngine;

public class TimeChallengeGameState : GameLoopState
{
    public TimeChallengeGameState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
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