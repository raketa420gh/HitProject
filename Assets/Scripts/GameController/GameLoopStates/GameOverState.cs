using UnityEngine;

public class GameOverState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly GameOverUIPanel _gameOverPanel;
    
    public GameOverState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _gameOverPanel = gameLoopStateMachine.Parent.UIController.GameOverPanel;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _gameOverPanel.OnHomeButtonClicked += HandleHomeButtonClickEvent;
        _gameOverPanel.Show();
    }

    public override void OnStateDisabled()
    {
        _gameOverPanel.OnHomeButtonClicked -= HandleHomeButtonClickEvent;
        _gameOverPanel.Hide();
    }

    public override void Update()
    {
        
    }
    
    private void HandleHomeButtonClickEvent()
    {
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.MainMenu);
    }
}