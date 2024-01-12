using UnityEngine;

public class LevelCompleteState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly IUIController _uiController;
    private PlayerGameSessionStats _playerGameSessionStats;
    private LevelCompleteUIPanel _levelCompletePanel;

    public LevelCompleteState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _uiController = gameLoopStateMachine.Parent.UIController;
        _levelCompletePanel = _uiController.LevelCompletePanel;
    }

    public override void OnStateRegistered()
    {
        
    }

    public override void OnStateActivated()
    {
        Debug.Log("Complete solo game level");
        
        _levelCompletePanel.OnHomeButtonClicked += HandleHomeButtonClickEvent;
        _levelCompletePanel.OnReplayButtonClicked += HandleReplayButtonClickEvent;
        _levelCompletePanel.OnNextLevelButtonClicked += HandleNextLevelButtonClickEvent;
    }

    public override void OnStateDisabled()
    {
        _levelCompletePanel.OnHomeButtonClicked -= HandleHomeButtonClickEvent;
        _levelCompletePanel.OnReplayButtonClicked -= HandleReplayButtonClickEvent;
        _levelCompletePanel.OnNextLevelButtonClicked -= HandleNextLevelButtonClickEvent;
        
        _levelCompletePanel.Hide();
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
        
    }
    
    private void HandleNextLevelButtonClickEvent()
    {
        
    }
}