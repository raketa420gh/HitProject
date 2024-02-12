using UnityEngine;

public class LevelCompleteState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly LevelCompleteUIPanel _levelCompletePanel;
    private readonly ISaveService _saveService;
    private readonly ILevelController _levelController;
    private readonly IPowerUpsController _powerUpsController;
    private readonly IUIController _uiController;
    private PlayerGameSessionStats _playerGameSessionStats;

    public LevelCompleteState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _saveService = _gameLoopStateMachine.Parent.SaveService;
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
        
        _levelCompletePanel.OnHomeButtonClicked += HandleHomeButtonClickEvent;
        _levelCompletePanel.OnNextLevelButtonClicked += HandleNextLevelButtonClickEvent;
        
        _levelCompletePanel.Show();
        _powerUpsController.SetPowerUpsUsableState(false);
        
        _saveService.ForceSave();
    }

    public override void OnStateDisabled()
    {
        _levelCompletePanel.OnHomeButtonClicked -= HandleHomeButtonClickEvent;
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
    
    private void HandleNextLevelButtonClickEvent()
    {
        
    }
}