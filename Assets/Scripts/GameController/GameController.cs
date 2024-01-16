using System;
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
    private ILevelController _levelController;
    private GameLoopStateMachine _gameLoopStateMachine;
    private GameModeType _gameModeType = GameModeType.Solo;

    public List<QuestionData> QuestionsDatabase => _questionsDatabase;
    public ISaveService SaveService => _saveService;
    public IFactory Factory => _factory;
    public ICurrenciesController CurrenciesController => _currenciesController;
    public IUIController UIController => _uiController;
    public ILevelController LevelController => _levelController;

    [Inject]
    public void Construct(ISaveService saveService, IFactory factory, ICurrenciesController currenciesController,
        IUIController uiController, ILevelController levelController)
    {
        _saveService = saveService;
        _factory = factory;
        _currenciesController = currenciesController;
        _uiController = uiController;
        _levelController = levelController;
    }

    private void Start()
    {
        InitializeGameLoopStateMachine();
    }


    private void OnEnable()
    {
        _levelController.OnRollDicePanelPlayButtonClicked += HandleRollDicePlayButtonEvent;
    }

    private void OnDisable()
    {
        _levelController.OnRollDicePanelPlayButtonClicked -= HandleRollDicePlayButtonEvent;
    }

    private void Update()
    {
        _gameLoopStateMachine?.ActiveState?.Update();
        
        if (Input.GetKeyDown(KeyCode.M))
            _currenciesController.Add(Currency.Type.Money, 100);

        if (Input.GetKeyDown(KeyCode.S))
            _saveService.ForceSave();

        if (Input.GetKeyDown(KeyCode.I))
            _saveService.GetInfo();
    }

    private void InitializeGameLoopStateMachine()
    {
        _gameLoopStateMachine = new GameLoopStateMachine();
        _gameLoopStateMachine.Initialise(this, GameLoopStateMachine.State.Initialize);
    }

    private void HandleRollDicePlayButtonEvent(GameModeType gameModeType)
    {
        if (gameModeType == GameModeType.Solo)
            _gameLoopStateMachine.SetState(GameLoopStateMachine.State.SoloGame);
    }
}