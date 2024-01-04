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
        _saveService = _stateMachine.Parent.SaveService;
        _factory = _stateMachine.Parent.Factory;
        _currenciesController = _stateMachine.Parent.CurrenciesController;
        
        Debug.Log("Initialise state registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log("Initialize state entered");
        
        _saveService.Initialise(Time.time, false, false);
        _factory.Initialize();
        _currenciesController.Initialise(_saveService);

        Debug.Log("Game systems initialized");
        
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.MainMenu);
    }

    public override void OnStateDisabled()
    {
    }

    public override void Update()
    {
    }
}