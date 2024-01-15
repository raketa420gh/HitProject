using UnityEngine;

public class InitializeState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private ISaveService _saveService;
    private IFactory _factory;
    private ICurrenciesController _currenciesController;
    private ILevelController _levelController;

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
        
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _saveService.Initialise(Time.time, false, false);
        _factory.Initialize();
        _currenciesController.Initialise(_saveService);

        Debug.Log("Game systems initialized");
        
        _levelController.InitializeLevelSave();

        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.CreatePlayer);
    }

    public override void OnStateDisabled()
    {
    }

    public override void Update()
    {
    }

    private void InitializeGame()
    {
        LevelSave levelSave = _saveService.GetSaveObject<LevelSave>("PlayerSave") ?? new LevelSave();
        LoadPlayerDataFromSave(levelSave);
    }

    private void LoadPlayerDataFromSave(LevelSave levelSave)
    {
    }
}