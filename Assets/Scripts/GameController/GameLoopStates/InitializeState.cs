using UnityEngine;

public class InitializeState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private ISaveService _saveService;
    private LevelSave _levelSave;
    private IFactory _factory;
    private ICurrenciesController _currenciesController;
    private ILevelController _levelController;
    private IUIController _uiController;

    public InitializeState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
    }

    public override void OnStateRegistered()
    {
        _saveService = _gameLoopStateMachine.Parent.SaveService;
        _factory = _gameLoopStateMachine.Parent.Factory;
        _currenciesController = _gameLoopStateMachine.Parent.CurrenciesController;
        _levelController = _gameLoopStateMachine.Parent.LevelController;
        _uiController = _gameLoopStateMachine.Parent.UIController;
        
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _saveService.Initialise(Time.time, false);
        _factory.Initialize();
        _currenciesController.Initialise(_saveService);
        _levelController.InitializeLevelSave();
        
        Debug.Log("Game systems initialized");
        
        InitializeStartGameState();
    }

    public override void OnStateDisabled()
    {
    }

    public override void Update()
    {
    }

    private void InitializeStartGameState()
    {
        _levelSave = _saveService.GetSaveObject<LevelSave>("save");

        if (_levelSave != null && _levelSave.PlayerName != null && _levelSave.PlayerName != "PLAYER NAME")
        {
            _uiController.PlayersInfoPanel.YouPlayerPanel.SetPlayerInfo(_levelSave.PlayerName);
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.MainMenu);
        }
        else
        {
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.CreatePlayer);
        }
    }
}