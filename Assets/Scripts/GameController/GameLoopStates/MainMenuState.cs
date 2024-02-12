using UnityEngine;

public class MainMenuState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;

    public MainMenuState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        InitializeMainMenu();
    }

    public override void OnStateDisabled()
    {
    }

    public override void Update()
    {
    }

    private void InitializeMainMenu()
    {
    }
}