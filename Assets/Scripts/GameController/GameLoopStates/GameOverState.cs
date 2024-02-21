using UnityEngine;

public class GameOverState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly IPlayerController _playerController;
    private readonly ILevelController _levelController;
    private readonly IUIController _uiController;
    
    public GameOverState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _playerController = _gameLoopStateMachine.Parent.PlayerController;
        _levelController = _gameLoopStateMachine.Parent.LevelController;
        _uiController = _gameLoopStateMachine.Parent.UIController;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _playerController.StopMove();
        _uiController.HudPanel.Hide();
        _uiController.LosePanel.Show();
        
        Debug.Log("LOSE");
    }

    public override void OnStateDisabled()
    {
    }

    public override void Update()
    {
        
    }
    
    private void HandleRestartButtonClickEvent()
    {
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.MainMenu);
    }
}