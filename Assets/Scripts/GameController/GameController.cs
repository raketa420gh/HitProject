using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    private GameLoopStateMachine _gameLoopStateMachine;
    private ISaveService _saveService;
    private IFactory _factory;
    private ICurrenciesController _currenciesController;
    private IUIController _uiController;
    private ILevelController _levelController;
    private IPowerUpsController _powerUpsController;
    private IPlayerController _playerController;
    private TimeCounter _timeCounter;
    
    public ISaveService SaveService => _saveService;
    public IFactory Factory => _factory;
    public ICurrenciesController CurrenciesController => _currenciesController;
    public IUIController UIController => _uiController;
    public ILevelController LevelController => _levelController;
    public IPowerUpsController PowerUpsController => _powerUpsController;
    public IPlayerController PlayerController => _playerController;
    public TimeCounter TimeCounter => _timeCounter;

    [Inject]
    public void Construct(ISaveService saveService, IFactory factory, ICurrenciesController currenciesController,
        IUIController uiController, ILevelController levelController, IPowerUpsController powerUpsController,
        IPlayerController playerController, TimeCounter timeCounter)
    {
        _saveService = saveService;
        _factory = factory;
        _currenciesController = currenciesController;
        _uiController = uiController;
        _levelController = levelController;
        _powerUpsController = powerUpsController;
        _playerController = playerController;
        _timeCounter = timeCounter;
    }

    private void Start()
    {
        InitializeGameLoopStateMachine();
    }

    private void Update()
    {
        _gameLoopStateMachine?.ActiveState?.Update();

        if (Input.GetKeyDown(KeyCode.M))
        {
            _currenciesController.Add(Currency.Type.Money, 100); 
        }
        
        if (Input.GetKeyDown(KeyCode.S))
            _saveService.ForceSave();

        if (Input.GetKeyDown(KeyCode.I))
        {
            _saveService.GetInfo();
            Debug.Log($"Soft currency = {_currenciesController.GetCurrency(Currency.Type.Money).Amount}");
        }
           
    }

    private void InitializeGameLoopStateMachine()
    {
        _gameLoopStateMachine = new GameLoopStateMachine();
        _gameLoopStateMachine.Initialise(this, GameLoopStateMachine.State.Initialize);
    }
}