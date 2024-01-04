using UnityEngine;

public class MainMenuState : GameLoopState
{
    public MainMenuState(GameLoopStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnStateRegistered()
    {
        Debug.Log("Main menu state registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log("Main menu state entered");
    }

    public override void OnStateDisabled()
    {
        
    }

    public override void Update()
    {
        
    }
}