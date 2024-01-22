using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimeChallengeGameState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly IUIController _uiController;
    private readonly InGameUIPanel _inGamePanel;
    private readonly LevelCompleteUIPanel _levelCompletePanel;
    private readonly GameOverUIPanel _gameOverPanel;
    private readonly AnswerUIButton[] _answerUIButtons;
    private readonly ILevelController _levelController;
    private int _currentCorrectAnswerIndex;
    private int _activeLevelNumber;
    private List<QuestionData> _questionsDatabase = new List<QuestionData>();
    
    private bool _isResultViewing;
    private float _resultViewTimer;
    private float _resultViewTime = 2f;

    private bool _isTurnTimerActive;
    private float _turnTimer;
    private float _turnTime = 5f;
    private float _turnTimeProgressNormalized;

    private bool _isGlobalTimerActive;
    private float _globalTimer;
    private float _globalTime = 300;
    private float _globalTimeProgressNormalized;
    
    private PlayerGameSessionStats _playerGameSessionStats;
    
    public TimeChallengeGameState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _uiController = _gameLoopStateMachine.Parent.UIController;
        _inGamePanel = _uiController.InGamePanel;
        _levelCompletePanel = _uiController.LevelCompletePanel;
        _gameOverPanel = _uiController.GameOverPanel;
        _answerUIButtons = _inGamePanel.QuestionPanel.AnswerUIButtons;
        _levelController = _gameLoopStateMachine.Parent.LevelController;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.OnClicked += HandleAnswerClickEvent;
        
        _inGamePanel.TurnTimeProgressBar.Show();
        _inGamePanel.GlobalTimeProgressBar.Show();
        _isGlobalTimerActive = true;
        _isTurnTimerActive = true;

        ResetResultViewTimer();
        InitializePlayerSession();
        InitializeQuestions();
        ActivateNextQuestion();
        
        _inGamePanel.Show();
    }

    public override void OnStateDisabled()
    {
        _playerGameSessionStats.ResetAll();
        _inGamePanel.Hide();
    }

    public override void Update()
    {
        if (_isResultViewing)
        {
            _resultViewTimer += Time.deltaTime;

            if (_resultViewTimer > _resultViewTime)
            {
                ActivateNextQuestion();
            }
        }

        if (_isGlobalTimerActive)
        {
            _globalTimer += Time.deltaTime;

            _globalTimeProgressNormalized = _globalTimer / _globalTime;
            _inGamePanel.GlobalTimeProgressBar.SetFillAmount(_globalTimeProgressNormalized);

            if (_globalTimer > _globalTime)
            {
                CompleteTimeChallengeLevel();
            }
        }

        if (_isTurnTimerActive)
        {
            _turnTimer += Time.deltaTime;
            
            _turnTimeProgressNormalized = _turnTimer / _turnTime;
            _inGamePanel.TurnTimeProgressBar.SetFillAmount(_turnTimeProgressNormalized);

            if (_turnTimer > _turnTime)
            {
                GameOverTimeChallenge();
            }
        }
    }

    private void ResetResultViewTimer()
    {
        _resultViewTimer = 0f;
        _isResultViewing = false;
    }

    private void ResetGlobalTimer()
    {
        _globalTimer = 0f;
        _isGlobalTimerActive = false;
    }

    private void ResetTurnTimer()
    {
        _turnTimer = 0f;
        _isTurnTimerActive = false;
    }

    private void InitializePlayerSession()
    {
        _playerGameSessionStats ??= new PlayerGameSessionStats();
        _playerGameSessionStats.OnUpdated += HandlePlayerGameSessionStatsUpdateEvent;
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScoreView(true);
    }

    private void InitializeQuestions()
    {
        _questionsDatabase = _gameLoopStateMachine.Parent.QuestionsDatabase;
    }

    private void ActivateNextQuestion()
    {
        ResetResultViewTimer();
        _isTurnTimerActive = true;
        
        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.Reset();

        var rQuestionDataIndex = Random.Range(0, _questionsDatabase.Count);
        var currentQuestionData = _questionsDatabase[rQuestionDataIndex];

        _currentCorrectAnswerIndex = currentQuestionData.CorrectAnswerIndex;
        _inGamePanel.QuestionPanel.SetQuestion(currentQuestionData);

        _questionsDatabase.Remove(currentQuestionData);

        if (_questionsDatabase.Count == 0)
        {
            Debug.Log("Reset questions");
            InitializeQuestions();
        }
        
        _inGamePanel.QuestionPanel.HideResultView();
    }

    private void GameOverTimeChallenge()
    {
        _gameOverPanel.GameStatsPanel.SetScore(_playerGameSessionStats.CategoryPoints);
        _gameOverPanel.GameStatsPanel.SetTime((int)_globalTimer);
        
        ResetGlobalTimer();
        ResetTurnTimer();
        
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.GameOver);
    }

    private async UniTaskVoid GameOverTimeChallengeAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_resultViewTime));
        
        _gameOverPanel.GameStatsPanel.SetScore(_playerGameSessionStats.CategoryPoints);
        _gameOverPanel.GameStatsPanel.SetTime((int)_globalTimer);
        
        ResetGlobalTimer();
        ResetTurnTimer();
        
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.GameOver);
    }

    private void CompleteTimeChallengeLevel()
    {
        _levelCompletePanel.GameStatsPanel.SetScore(_playerGameSessionStats.CategoryPoints);
        _levelCompletePanel.GameStatsPanel.SetTime((int)_globalTime);
        _levelCompletePanel.GameStatsPanel.SetCoins(100);
        
        ResetGlobalTimer();
        ResetTurnTimer();
        
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.LevelComplete);
    }

    private async UniTaskVoid CompleteTimeChallengeLevelAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_resultViewTime));
        
        ResetGlobalTimer();
        ResetTurnTimer();
        
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.LevelComplete);
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
        
        ResetTurnTimer();
    }

    private void HandleCorrectAnswerAsync(int index)
    {
        Debug.Log("CORRECT ANSWER");

        _inGamePanel.QuestionPanel.ShowResultView(true);
        _answerUIButtons[index].SetAnswerViewResult(true);

        _playerGameSessionStats.AddCategoryPoint();
    }

    private void HandleWrongAnswer(int index)
    {
        Debug.Log("WRONG ANSWER");

        _inGamePanel.QuestionPanel.ShowResultView(false);
        _answerUIButtons[index].SetAnswerViewResult(false);
        _answerUIButtons[_currentCorrectAnswerIndex].SetAnswerViewResult(true);

        GameOverTimeChallengeAsync();
    }

    private void HandlePlayerGameSessionStatsUpdateEvent(PlayerGameSessionStats playerGameSessionStats)
    {
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScore(_playerGameSessionStats.CategoryPoints);
    }
}