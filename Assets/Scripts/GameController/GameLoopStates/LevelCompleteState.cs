using UnityEngine;

public class LevelCompleteState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly LevelCompleteUIPanel _levelCompletePanel;
    private readonly ISaveService _saveService;
    private readonly ILevelController _levelController;
    private PlayerGameSessionStats _playerGameSessionStats;

    public LevelCompleteState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _levelCompletePanel = gameLoopStateMachine.Parent.UIController.LevelCompletePanel;
        _saveService = _gameLoopStateMachine.Parent.SaveService;
        _levelController = _gameLoopStateMachine.Parent.LevelController;
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
        
        _saveService.ForceSave();
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
        if (_levelController.CurrentGameMode == GameModeType.TimeChallenge)
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.TimeChallenge);
        
        if (_levelController.CurrentGameMode == GameModeType.Solo)
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.RollDice);
        
        if (_levelController.CurrentGameMode == GameModeType.Versus)
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.VersusGame);
    }
    
    private void HandleNextLevelButtonClickEvent()
    {
        
    }
}