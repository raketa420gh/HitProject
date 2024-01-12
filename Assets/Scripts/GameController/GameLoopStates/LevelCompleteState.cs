using UnityEngine;

public class LevelCompleteState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly LevelCompleteUIPanel _levelCompletePanel;
    private PlayerGameSessionStats _playerGameSessionStats;

    public LevelCompleteState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _levelCompletePanel = gameLoopStateMachine.Parent.UIController.LevelCompletePanel;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _levelCompletePanel.OnHomeButtonClicked += HandleHomeButtonClickEvent;
        _levelCompletePanel.OnReplayButtonClicked += HandleReplayButtonClickEvent;
        _levelCompletePanel.OnNextLevelButtonClicked += HandleNextLevelButtonClickEvent;
        
        _levelCompletePanel.Show();
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