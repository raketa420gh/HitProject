using UnityEngine;

public class InitializeState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private ISaveService _saveService;
    private IFactory _factory;
    private ICurrenciesController _currenciesController;

    public InitializeState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
    }

    public override void OnStateRegistered()
    {
        _saveService = _gameLoopStateMachine.Parent.SaveService;
        _factory = _gameLoopStateMachine.Parent.Factory;
        _currenciesController = _gameLoopStateMachine.Parent.CurrenciesController;
        
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _saveService.Initialise(Time.time, false, false);
        _factory.Initialize();
        _currenciesController.Initialise(_saveService);

        Debug.Log("Game systems initialized");

        _gameLoopStateMachine.SetState(global::GameLoopStateMachine.State.MainMenu);
    }

    public override void OnStateDisabled()
    {
    }

    public override void Update()
    {
    }

    private void InitializeGame()
    {
        PlayerSaveData playerSaveData = _saveService.GetSaveObject<PlayerSaveData>("PlayerSave") ?? new PlayerSaveData();
        LoadPlayerDataFromSave(playerSaveData);
    }

    private void LoadPlayerDataFromSave(PlayerSaveData playerSaveData)
    {
    }
}