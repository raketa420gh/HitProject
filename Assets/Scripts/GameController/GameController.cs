using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    private ISaveService _saveService;
    private IFactory _factory;
    private ICurrenciesController _currenciesController;
    private IUIController _uiController;
    private LevelController _levelController;
    private GameLoopStateMachine _gameLoopStateMachine;

    public ISaveService SaveService => _saveService;
    public IFactory Factory => _factory;
    public ICurrenciesController CurrenciesController => _currenciesController;
    public IUIController UIController => _uiController;

    [Inject]
    public void Construct(ISaveService saveService, IFactory factory, ICurrenciesController currenciesController,
        IUIController uiController, LevelController levelController)
    {
        _saveService = saveService;
        _factory = factory;
        _currenciesController = currenciesController;
        _uiController = uiController;
        _levelController = levelController;
    }

    private void OnEnable()
    {
        _uiController.GameModesPanel.OnSoloButtonClicked += HandleSoloGameStart;
        _uiController.GameModesPanel.OnVersusButtonClicked += HandleVersusGameStart;
        _uiController.GameModesPanel.OnTimeChallengeButtonClicked += HandleTimeChallengeGameStart;
    }

    private void OnDisable()
    {
        _uiController.GameModesPanel.OnSoloButtonClicked -= HandleSoloGameStart;
        _uiController.GameModesPanel.OnVersusButtonClicked -= HandleVersusGameStart;
        _uiController.GameModesPanel.OnTimeChallengeButtonClicked -= HandleTimeChallengeGameStart;
    }

    private void Start()
    {
        InitializeGameLoopStateMachine();
    }

    private void Update()
    {
        _gameLoopStateMachine?.ActiveState?.Update();
        
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

    private void HandleSoloGameStart()
    {
        Debug.Log($"START SOLO GAME");
    }

    private void HandleVersusGameStart()
    {
        Debug.Log($"START VERSUS GAME");
    }

    private void HandleTimeChallengeGameStart()
    {
        Debug.Log($"START TIME CHALLENGE GAME");
    }
}