using UnityEngine;

public class TestRunState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly IPlayerController _playerController;
    private readonly TimeCounter _timeCounter;
    
    public TestRunState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _playerController = _gameLoopStateMachine.Parent.PlayerController;
        _timeCounter = _gameLoopStateMachine.Parent.TimeCounter;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");

        _playerController.StartMoveForward();
        _playerController.EnableShooting();
        
        _timeCounter.StartTimeCounting();
    }

    public override void OnStateDisabled()
    {
        _playerController.StopMove();
        _playerController.DisableShooting();
    }

    public override void Update()
    {
        
    }
}