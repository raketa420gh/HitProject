using UnityEngine;

public class CreatePlayerState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly CreatePlayerUIPanel _createPlayerPanel;
    private readonly PlayersInfoUIPanel _playersInfoPanel;
    private readonly SelectIconUIPanel _selectIconPanel;

    public CreatePlayerState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _createPlayerPanel = _gameLoopStateMachine.Parent.UIController.CreatePlayerPanel;
        _playersInfoPanel = _gameLoopStateMachine.Parent.UIController.PlayersInfoPanel;
        _selectIconPanel = _gameLoopStateMachine.Parent.UIController.SelectIconPanel;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");

        _createPlayerPanel.OnNameConfirmButtonClicked += HandleNameConfirmButtonEvent;
        _createPlayerPanel.OnIconSelected += HandleIconSelectEvent;
        _createPlayerPanel.Show();
    }

    public override void OnStateDisabled()
    {
        _createPlayerPanel.OnNameConfirmButtonClicked -= HandleNameConfirmButtonEvent;
        _createPlayerPanel.OnIconSelected -= HandleIconSelectEvent;
        _createPlayerPanel.Hide();
    }

    public override void Update()
    {
        
    }

    private void HandleNameConfirmButtonEvent(string nameText)
    {
        _playersInfoPanel.YouPlayerPanel.SetPlayerName(nameText);
        _gameLoopStateMachine.Parent.SaveService.ForceSave();
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.MainMenu);
    }
    
    private void HandleIconSelectEvent(Sprite sprite, int iconNumber)
    {
        _playersInfoPanel.YouPlayerPanel.SetIcon(sprite);
        _playersInfoPanel.YouPlayerPanel.SetIconNumber(iconNumber);
        _selectIconPanel.Hide();
    }
}