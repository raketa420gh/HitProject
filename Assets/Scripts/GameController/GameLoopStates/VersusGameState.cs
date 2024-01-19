using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class VersusGameState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly ILevelController _levelController;
    private readonly IUIController _uiController;
    private readonly InGameUIPanel _inGamePanel;
    private readonly PlayersInfoUIPanel _playersInfoPanel;
    private readonly SelectPlayerTurnUIPanel _selectPlayerTurnPanel;
    private readonly AnswerUIButton[] _answerUIButtons;
    private List<QuestionData> _categoryQuestions = new List<QuestionData>();
    private int _currentCorrectAnswerIndex;
    private PlayerTurnType _currentPlayerTurnType = PlayerTurnType.You;
    private bool _isNewGame = true;
    
    private bool _isResultViewing;
    private float _resultViewTimer;
    private float _resultViewTime = 2f;

    private PlayerGameSessionStats _youPlayerGameSessionStats;
    private PlayerGameSessionStats _opponentPlayerGameSessionStats;

    public VersusGameState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _levelController = _gameLoopStateMachine.Parent.LevelController;
        _uiController = _gameLoopStateMachine.Parent.UIController;
        _inGamePanel = _uiController.InGamePanel;
        _playersInfoPanel = _uiController.PlayersInfoPanel;
        _selectPlayerTurnPanel = _uiController.SelectPlayerTurnPanel;
        _answerUIButtons = _inGamePanel.QuestionPanel.AnswerUIButtons;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _levelController.SetGameMode(GameModeType.Versus);
        InitializePlayersInfoPanel();

        if (_isNewGame)
        {
            _selectPlayerTurnPanel.OnPlayerTurnTypeSelected += HandlePlayerTurnTypeSelectEvent;
            _selectPlayerTurnPanel.RollDiceToSelectNewPlayerTurnType();
            _selectPlayerTurnPanel.Show();
        }
    }

    public override void OnStateDisabled()
    {
        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.OnClicked -= HandleAnswerClickEvent;
        
        _youPlayerGameSessionStats.OnUpdated -= HandleYouPlayerGameSessionStatsUpdateEvent;
        _selectPlayerTurnPanel.OnPlayerTurnTypeSelected -= HandlePlayerTurnTypeSelectEvent;
        
        _inGamePanel.Hide();
    }

    public override void Update()
    {
        if (_isResultViewing)
        {
            _resultViewTimer += Time.deltaTime;

            if (_resultViewTimer > _resultViewTime)
            {
                ResetResultViewTimer();
                ActivateNextQuestion();
                ChangeNextPlayerTurnType();
            }
        }
    }

    private void ResetResultViewTimer()
    {
        _resultViewTimer = 0f;
        _isResultViewing = false;
    }

    private void InitializePlayersSession()
    {
        _youPlayerGameSessionStats ??= new PlayerGameSessionStats();
        _youPlayerGameSessionStats.OnUpdated += HandleYouPlayerGameSessionStatsUpdateEvent;
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScoreView(true);
        
        _opponentPlayerGameSessionStats ??= new PlayerGameSessionStats();
        _opponentPlayerGameSessionStats.OnUpdated += HandleOpponentPlayerGameSessionStatsUpdateEvent;
        _uiController.PlayersInfoPanel.OpponentPlayerPanel.SetScoreView(true);
    }

    private void InitializePlayersInfoPanel()
    {
        _playersInfoPanel.YouPlayerPanel.SetScoreView(true);
        _playersInfoPanel.YouPlayerPanel.SetView(true);
        _playersInfoPanel.OpponentPlayerPanel.SetPlayerInfo("Opponent");
        _playersInfoPanel.OpponentPlayerPanel.SetScoreView(true);
        _playersInfoPanel.OpponentPlayerPanel.SetView(true);
        _playersInfoPanel.SetInfoText("");
    }

    private void InitializeQuestionsCategory()
    {
        _categoryQuestions = new List<QuestionData>();
        RollDiceState rollDiceState = (RollDiceState)_gameLoopStateMachine.GetState(GameLoopStateMachine.State.RollDice);
        
        List<QuestionData> questionsDatabase = _gameLoopStateMachine.Parent.QuestionsDatabase;

        for (int i = 0; i < questionsDatabase.Count; i++)
        {
            QuestionData questionData = questionsDatabase[i];
            
            if (questionData.CategoryType == rollDiceState.RolledCategoryType)
            {
                _categoryQuestions.Add(questionData);
            }
        }
        
        Debug.Log($"Questions initialized. Category = {rollDiceState.RolledCategoryType}, Questions count = {_categoryQuestions.Count}");
    }

    private void ActivateNextQuestion()
    {
        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.Reset();

        var rQuestionDataIndex = Random.Range(0, _categoryQuestions.Count);
        var currentQuestionData = _categoryQuestions[rQuestionDataIndex];

        _currentCorrectAnswerIndex = currentQuestionData.CorrectAnswerIndex;
        _inGamePanel.QuestionPanel.SetQuestion(currentQuestionData);

        _categoryQuestions.Remove(currentQuestionData);

        if (_categoryQuestions.Count == 0)
        {
            Debug.Log("Reset category questions");
            InitializeQuestionsCategory();
        }
        
        _inGamePanel.QuestionPanel.HideResultView();
    }

    private void ChangeNextPlayerTurnType()
    {
        DeactivateCurrentPlayerTurn();
        
        _currentPlayerTurnType = _currentPlayerTurnType == PlayerTurnType.You ? PlayerTurnType.Opponent : PlayerTurnType.You;
        
        Debug.Log($"PLAYER TURN CHANGED = {_currentPlayerTurnType}");

        ActivateCurrentPlayerTurnAsync();
    }

    private async UniTaskVoid ActivateCurrentPlayerTurnAsync()
    {
        _playersInfoPanel.SetInfoText(_currentPlayerTurnType + " turn!");

        if (_currentPlayerTurnType == PlayerTurnType.You)
        {
            foreach (AnswerUIButton answerUIButton in _answerUIButtons)
                answerUIButton.OnClicked += HandleAnswerClickEvent;
        }

        if (_currentPlayerTurnType == PlayerTurnType.Opponent)
        {
            float rDelay = Random.Range(2, 4);
            int rAnswerIndex = Random.Range(0, 3);
            
            await UniTask.Delay(TimeSpan.FromSeconds(rDelay));
            
            HandleAnswerClickEvent(rAnswerIndex);
        }
    }

    private void DeactivateCurrentPlayerTurn()
    {
        if (_currentPlayerTurnType == PlayerTurnType.You)
        {
            foreach (AnswerUIButton answerUIButton in _answerUIButtons)
                answerUIButton.OnClicked -= HandleAnswerClickEvent;
        }
    }

    private void HandleAnswerClickEvent(int index)
    {
        AnswerUIButton clickedAnswerButton = _answerUIButtons[index];
        AnswerUIButton correctAnswerButton = _answerUIButtons[_currentCorrectAnswerIndex];
        
        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.SetInteractable(false);

        if (index == _currentCorrectAnswerIndex)
            HandleCorrectAnswerAsync(index);
        else
            HandleWrongAnswer(index);

        clickedAnswerButton.transform.localScale = Vector3.one * 1.1f;
        correctAnswerButton.transform.localScale = Vector3.one * 1.1f;

        _isResultViewing = true;
    }

    private async UniTaskVoid HandleCorrectAnswerAsync(int index)
    {
        Debug.Log("CORRECT ANSWER");

        _inGamePanel.QuestionPanel.ShowResultView(true);
        _answerUIButtons[index].SetAnswerViewResult(true);

        if (_currentPlayerTurnType == PlayerTurnType.You)
        {
            _youPlayerGameSessionStats.AddCategoryPoint();

            if (_youPlayerGameSessionStats.CategoryPoints >= 3)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_resultViewTime));
                
                _youPlayerGameSessionStats.ResetAll();
                _opponentPlayerGameSessionStats.ResetAll();
                _gameLoopStateMachine.SetState(GameLoopStateMachine.State.LevelComplete);
            }
        }

        if (_currentPlayerTurnType == PlayerTurnType.Opponent)
        {
            _opponentPlayerGameSessionStats.AddCategoryPoint();
            
            if (_opponentPlayerGameSessionStats.CategoryPoints >= 3)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_resultViewTime));
                
                _youPlayerGameSessionStats.ResetAll();
                _opponentPlayerGameSessionStats.ResetAll();
                _gameLoopStateMachine.SetState(GameLoopStateMachine.State.GameOver);
            }
        }
    }

    private async UniTaskVoid HandleWrongAnswer(int index)
    {
        Debug.Log("WRONG ANSWER");

        _inGamePanel.QuestionPanel.ShowResultView(false);
        _answerUIButtons[index].SetAnswerViewResult(false);
        _answerUIButtons[_currentCorrectAnswerIndex].SetAnswerViewResult(true);
    }

    private void HandleYouPlayerGameSessionStatsUpdateEvent(PlayerGameSessionStats playerGameSessionStats)
    {
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScore(_youPlayerGameSessionStats.CategoryPoints);
    }

    private void HandleOpponentPlayerGameSessionStatsUpdateEvent(PlayerGameSessionStats playerGameSessionStats)
    {
        _uiController.PlayersInfoPanel.OpponentPlayerPanel.SetScore(_opponentPlayerGameSessionStats.CategoryPoints);
    }

    private void HandlePlayerTurnTypeSelectEvent(PlayerTurnType playerTurnType)
    {
        _currentPlayerTurnType = playerTurnType;

        ResetResultViewTimer();
        InitializePlayersSession();
        InitializeQuestionsCategory();
        ActivateNextQuestion();
        ActivateCurrentPlayerTurnAsync();
        
        _inGamePanel.Show();
    }
}