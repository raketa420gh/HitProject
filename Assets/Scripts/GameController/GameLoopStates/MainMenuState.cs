using UnityEngine;

public class MainMenuState : GameLoopState
{
    private IUIController _uiController;

    public MainMenuState(GameLoopStateMachine stateMachine) : base(stateMachine)
    {
        _uiController = _stateMachine.Parent.UIController;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _uiController.PlayersInfoPanel.OpponentPlayerPanel.SetView(false);
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScoreView(false);
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetView(true);
    }

    public override void OnStateDisabled()
    {
        
    }

    public override void Update()
    {
        
    }
}