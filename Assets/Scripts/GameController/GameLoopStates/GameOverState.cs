using UnityEngine;

public class GameOverState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly GameOverUIPanel _gameOverPanel;
    private readonly ILevelController _levelController;
    private readonly IPowerUpsController _powerUpsController;
    private readonly IUIController _uiController;
    
    public GameOverState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _levelController = _gameLoopStateMachine.Parent.LevelController;
        _powerUpsController = _gameLoopStateMachine.Parent.PowerUpsController;
        _uiController = _gameLoopStateMachine.Parent.UIController;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _gameOverPanel.GameStatsPanel.Show();
        _gameOverPanel.OnHomeButtonClicked += HandleHomeButtonClickEvent;
        _gameOverPanel.OnReplayButtonClicked += HandleReplayButtonClickEvent;
        _gameOverPanel.Show();
        _powerUpsController.SetPowerUpsUsableState(false);
        
    }

    public override void OnStateDisabled()
    {
        _gameOverPanel.GameStatsPanel.Hide();
        _gameOverPanel.OnHomeButtonClicked -= HandleHomeButtonClickEvent;
        _gameOverPanel.OnReplayButtonClicked -= HandleReplayButtonClickEvent;
        _gameOverPanel.Hide();
    }

    public override void Update()
    {
        
    }
    
    private void HandleHomeButtonClickEvent()
    {
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.MainMenu);
    }
    
    private void HandleReplayButtonClickEvent()
    {
        if (_levelController.CurrentGameMode == GameModeType.TimeChallenge)
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.TimeChallenge);
        
        if (_levelController.CurrentGameMode == GameModeType.Solo)
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.RollDice);
        
        if (_levelController.CurrentGameMode == GameModeType.Versus)
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.RollDice);
    }
}