using UnityEngine;

public class InitializeState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private ISaveService _saveService;
    private LevelSave _levelSave;
    private IFactory _factory;
    private ICurrenciesController _currenciesController;
    private ILevelController _levelController;
    private IPowerUpsController _powerUpsController;
    private IUIController _uiController;
    private ParallaxController _parallaxController;

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
        _parallaxController = _gameLoopStateMachine.Parent.ParallaxController;
        _powerUpsController = _gameLoopStateMachine.Parent.PowerUpsController;

        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");

        _saveService.Initialise(Time.time, false);
        _factory.Initialize();
        _currenciesController.Initialise(_saveService);
        _powerUpsController.Initialize(_currenciesController);
        _powerUpsController.LoadPowerUps(_saveService);
        _parallaxController.Initialize();
        _levelController.InitializeLevelSave();
        _uiController.ItemsPopup.InitializeCurrencies(_currenciesController);
        _uiController.StorePopup.Initialize(_currenciesController);
        _uiController.CreatePlayerPanel.SelectIconPanel.InitializeSlots();

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

        Debug.Log($"Try to load level save. {_levelSave != null}, PlayerName = {_levelSave?.PlayerName}");

        if (_levelSave != null && _levelSave.PlayerName != null)
        {
            SetLoadedPlayerInfoAndSave();
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.MainMenu);
        }
        else
        {
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.CreatePlayer);
        }
    }

    private void SetLoadedPlayerInfoAndSave()
    {
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetPlayerName(_levelSave.PlayerName);
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetIconNumber(_levelSave.IconNumber);
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetIcon(_uiController.CreatePlayerPanel.SelectIconPanel
            .GetSprite(_levelSave.IconNumber));
        
        _saveService.ForceSave();
    }
}