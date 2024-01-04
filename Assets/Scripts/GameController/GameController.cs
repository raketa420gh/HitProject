using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    private ISaveService _saveService;
    private IFactory _factory;
    private ICurrenciesController _currenciesController;
    private LevelController _levelController;
    private GameLoopStateMachine _gameLoopStateMachine;

    public ISaveService SaveService => _saveService;
    public IFactory Factory => _factory;
    public ICurrenciesController CurrenciesController => _currenciesController;

    [Inject]
    public void Construct(ISaveService saveService, IFactory factory, ICurrenciesController currenciesController,
        LevelController levelController)
    {
        _saveService = saveService;
        _factory = factory;
        _currenciesController = currenciesController;
        _levelController = levelController;
    }

    private void Start()
    {
        InitializeGameLoopStateMachine();
    }

    private void Update()
    {
        //_gameLoopStatesController?.ActiveState?.Update();
        
        if (Input.GetKeyDown(KeyCode.M))
            _currenciesController.Add(Currency.Type.Money, 100);

        if (Input.GetKeyDown(KeyCode.S))
            _saveService.Save();
    }

    private void InitializeGameLoopStateMachine()
    {
        _gameLoopStateMachine = new GameLoopStateMachine();
        _gameLoopStateMachine.Initialise(this, GameLoopStateMachine.State.Initialize);
    }
}