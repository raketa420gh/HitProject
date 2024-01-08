using UnityEngine;

public class MainMenuState : GameLoopState
{
    private readonly IUIController _uiController;
    private GameModesUIPanel _gameModesPanel;

    public MainMenuState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _uiController = gameLoopStateMachine.Parent.UIController;
        _gameModesPanel = _uiController.GameModesPanel;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");

        InitializeMainMenu();
        InitializePlayerInfoPanelsView();
    }

    public override void OnStateDisabled()
    {
        _gameModesPanel.Hide();
    }

    public override void Update()
    {
        
    }

    private void InitializePlayerInfoPanelsView()
    {
        _uiController.PlayersInfoPanel.OpponentPlayerPanel.SetView(false);
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScoreView(false);
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetView(true);
        _uiController.PlayersInfoPanel.gameObject.SetActive(true);
    }

    private void InitializeMainMenu()
    {
        _uiController.MainMenuPanel.SetStartButtonObjectView(true);
    }
}