using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<QuestionData> _questionsDatabase = new List<QuestionData>();
    private ISaveService _saveService;
    private IFactory _factory;
    private ICurrenciesController _currenciesController;
    private IUIController _uiController;
    private LevelController _levelController;
    private GameLoopStateMachine _gameLoopStateMachine;
    private GameModeType _gameModeType = GameModeType.Solo;

    public List<QuestionData> QuestionsDatabase => _questionsDatabase;
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
        _uiController.GameModesPanel.OnSoloButtonClicked += HandleSoloGameStartButtonEvent;
        _uiController.GameModesPanel.OnVersusButtonClicked += HandleVersusGameStartButtonEvent;
        _uiController.GameModesPanel.OnTimeChallengeButtonClicked += HandleTimeChallengeGameStartButtonEvent;
    }

    private void OnDisable()
    {
        _uiController.GameModesPanel.OnSoloButtonClicked -= HandleSoloGameStartButtonEvent;
        _uiController.GameModesPanel.OnVersusButtonClicked -= HandleVersusGameStartButtonEvent;
        _uiController.GameModesPanel.OnTimeChallengeButtonClicked -= HandleTimeChallengeGameStartButtonEvent;
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

    public void StartGameMode()
    {
        if (_gameModeType == GameModeType.Solo)
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.SoloGame);
    }
    
    private void SetGameMode(GameModeType type)
    {
        _gameModeType = type;
    }

    private void InitializeGameLoopStateMachine()
    {
        _gameLoopStateMachine = new GameLoopStateMachine();
        _gameLoopStateMachine.Initialise(this, GameLoopStateMachine.State.Initialize);
    }

    private void HandleSoloGameStartButtonEvent()
    {
        Debug.Log($"START SOLO GAME");
        
        SetGameMode(GameModeType.Solo);
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.RollDice);
    }

    private void HandleVersusGameStartButtonEvent()
    {
        Debug.Log($"START VERSUS GAME");
        
        SetGameMode(GameModeType.Versus);
    }

    private void HandleTimeChallengeGameStartButtonEvent()
    {
        Debug.Log($"START TIME CHALLENGE GAME");
        
        SetGameMode(GameModeType.TimeChallenge);
    }
}