using UnityEngine;

public class InitializeState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private ISaveService _saveService;
    private PlayerSave _playerSave;
    private IFactory _factory;
    private ICurrenciesController _currenciesController;
    private ILevelController _levelController;
    private IPowerUpsController _powerUpsController;
    private IUIController _uiController;
    private IPlayerController _playerController;
    private TimeCounter _timeCounter;
    private IDestroyableObjectsController _destroyableObjectsController;

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
        _powerUpsController = _gameLoopStateMachine.Parent.PowerUpsController;
        _playerController = _gameLoopStateMachine.Parent.PlayerController;
        _timeCounter = _gameLoopStateMachine.Parent.TimeCounter;
        _destroyableObjectsController = _gameLoopStateMachine.Parent.DestroyableObjectsController;

        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");

        _saveService.Initialise(Time.time, false);
        _factory.Initialize();
        _currenciesController.Initialise(_saveService);
        _levelController.InitializeLevelSave();
        _playerController.Initialize(_factory, _uiController);
        _timeCounter.Initialize(_uiController.HudPanel.GameTimerView);
        _destroyableObjectsController.Initialize(_playerController);

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
        _playerSave = _saveService.GetSaveObject<PlayerSave>("save");

        Debug.Log($"Try to load level save. {_playerSave != null}");
        
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.TestRun);
    }
}