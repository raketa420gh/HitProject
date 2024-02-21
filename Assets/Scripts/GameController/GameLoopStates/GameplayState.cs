using UnityEngine;

public class GameplayState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly IPlayerController _playerController;
    private readonly IDestroyableObjectsController _destroyableObjectsController;
    private readonly TimeCounter _timeCounter;
    private readonly IUIController _uiController;
    
    public GameplayState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _playerController = _gameLoopStateMachine.Parent.PlayerController;
        _destroyableObjectsController = _gameLoopStateMachine.Parent.DestroyableObjectsController;
        _timeCounter = _gameLoopStateMachine.Parent.TimeCounter;
        _uiController = _gameLoopStateMachine.Parent.UIController;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");

        _destroyableObjectsController.OnObstacleCollidePlayer += HandleObstacleCollidePlayerEvent;
        _playerController.OnProjectilesEnd += HandleProjectilesEndEvent;

        _playerController.StartMoveForward();
        _playerController.EnableShooting();
        
        _timeCounter.StartTimeCounting();
        _uiController.HudPanel.Show();
    }

    public override void OnStateDisabled()
    {
        _destroyableObjectsController.OnObstacleCollidePlayer -= HandleObstacleCollidePlayerEvent;
        _playerController.OnProjectilesEnd -= HandleProjectilesEndEvent;
        
        _playerController.StopMove();
        _playerController.DisableShooting();
    }

    public override void Update()
    {
        
    }

    private void HandleObstacleCollidePlayerEvent()
    {
        _playerController.RemoveProjectiles(5);
    }

    private void HandleProjectilesEndEvent()
    {
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.GameOver);
    }
}