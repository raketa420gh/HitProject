using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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
    private readonly ICurrenciesController _currenciesController;
    private readonly ParallaxController _parallaxController;
    private readonly IPowerUpsController _powerUpsController;
    private int _currentCorrectAnswerIndex;
    private int _activeLevelNumber;
    private List<QuestionData> _questionsDatabase = new List<QuestionData>();
    private PlayerGameSessionStats _playerGameSessionStats;
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
    private int _secondChancesCount;
    
    public TimeChallengeGameState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _uiController = _gameLoopStateMachine.Parent.UIController;
        _inGamePanel = _uiController.InGamePanel;
        _levelCompletePanel = _uiController.LevelCompletePanel;
        _gameOverPanel = _uiController.GameOverPanel;
        _answerUIButtons = _inGamePanel.QuestionPanel.AnswerUIButtons;
        _currenciesController = _gameLoopStateMachine.Parent.CurrenciesController;
        _parallaxController = _gameLoopStateMachine.Parent.ParallaxController;
        _powerUpsController = _gameLoopStateMachine.Parent.PowerUpsController;
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
        
        _powerUpsController.OnPowerUpActivated += HandlePowerUpActivateEvent;
        
        _inGamePanel.TurnTimeProgressBar.Show();
        _inGamePanel.GlobalTimeProgressBar.Show();
        _isGlobalTimerActive = true;
        _isTurnTimerActive = true;
        _parallaxController.EnableParallax();
        _parallaxController.ResetPositions();
        ResetResultViewTimer();
        InitializePlayerSession();
        InitializeQuestions();
        ActivateNextQuestion();
        _inGamePanel.Show();
        _uiController.MainMenuPanel.ShowOnlyItemsButton();
    }

    public override void OnStateDisabled()
    {
        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.OnClicked -= HandleAnswerClickEvent;
        
        _powerUpsController.OnPowerUpActivated -= HandlePowerUpActivateEvent;
        _parallaxController.ResetPositions();
        _parallaxController.DisableParallax();
        _playerGameSessionStats.ResetAll();
        _inGamePanel.Hide();
        _uiController.MainMenuPanel.ShowAllButtons();
    }

    public override void Update()
    {
        if (_isResultViewing)
        {
            _resultViewTimer += Time.deltaTime;

            if (_resultViewTimer > _resultViewTime)
            {
                ActivateNextQuestionAnimation();
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
    
    private void ResetSecondChance()
    {
        _secondChancesCount = 0;
    }

    private void InitializePlayerSession()
    {
        _playerGameSessionStats ??= new PlayerGameSessionStats();
        _playerGameSessionStats.OnUpdated += HandlePlayerGameSessionStatsUpdateEvent;
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScoreView(true);
    }

    private void InitializeQuestions()
    {
        _questionsDatabase = new(_gameLoopStateMachine.Parent.QuestionsDatabase);
        
        Debug.Log($"Questions initialized. Questions count = {_questionsDatabase.Count}");
    }    
    
    private void ActivateNextQuestionAnimation()
    {
        ResetResultViewTimer();
        ResetTurnTimer();
        
        RectTransform questionPanelRect = _inGamePanel.QuestionPanel.CurrentQuestionPanelRect;
        
        float screenWidth = 1920;
        float distanceToMove = -screenWidth;
        float animationTime = 2f;
        
        _parallaxController.DoParallaxHorizontalStep(20, animationTime);
        
        TweenerCore<Vector2, Vector2, VectorOptions> tween = questionPanelRect.DOAnchorPosX(questionPanelRect.anchoredPosition.x + distanceToMove, animationTime);

        tween.onComplete += ActivateNextQuestion;
    }

    private void ActivateNextQuestion()
    {
        _inGamePanel.QuestionPanel.CurrentQuestionPanelRect.DOAnchorPosX(0, 0);
        _isTurnTimerActive = true;
        _turnTime = 5f;
        
        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.Reset();

        int rQuestionDataIndex = Random.Range(0, _questionsDatabase.Count);
        QuestionData currentQuestionData = _questionsDatabase[rQuestionDataIndex];

        _currentCorrectAnswerIndex = currentQuestionData.CorrectAnswerIndex;
        _inGamePanel.QuestionPanel.SetQuestion(currentQuestionData);

        _questionsDatabase.Remove(currentQuestionData);

        if (_questionsDatabase.Count == 0)
        {
            Debug.Log("Reset questions");
            InitializeQuestions();
        }
        
        _inGamePanel.QuestionPanel.HideResultView();
        _powerUpsController.SetPowerUpsUsableState(true);
    }
    
    private List<AnswerUIButton> GetTwoRandomIncorrectAnswersUiButtons()
    {
        List<AnswerUIButton> twoIncorrectAnswersUiButtons = new List<AnswerUIButton>();
        List<AnswerUIButton> allIncorrectAnswersUiButtons = _answerUIButtons
            .Where(answerUIButton => answerUIButton != _answerUIButtons[_currentCorrectAnswerIndex]).ToList();

        int rIndex = Random.Range(0, allIncorrectAnswersUiButtons.Count);
        twoIncorrectAnswersUiButtons.Add(allIncorrectAnswersUiButtons[rIndex]);
        allIncorrectAnswersUiButtons.Remove(allIncorrectAnswersUiButtons[rIndex]);
        rIndex = Random.Range(0, allIncorrectAnswersUiButtons.Count);
        twoIncorrectAnswersUiButtons.Add(allIncorrectAnswersUiButtons[rIndex]);
        allIncorrectAnswersUiButtons.Clear();
        
        return twoIncorrectAnswersUiButtons;
    }

    private void GameOverTimeChallenge()
    {
        _gameOverPanel.GameStatsPanel.SetScore(_playerGameSessionStats.CategoryPoints);
        _gameOverPanel.GameStatsPanel.SetTime((int)_globalTimer);
        
        ResetGlobalTimer();
        ResetTurnTimer();
        ResetSecondChance();
        
        _powerUpsController.SetPowerUpsUsableState(false);
        
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.GameOver);
    }

    private async UniTaskVoid GameOverTimeChallengeAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_resultViewTime));
        
        GameOverTimeChallenge();
    }

    private void CompleteTimeChallengeLevel()
    {
        _levelCompletePanel.GameStatsPanel.SetScore(_playerGameSessionStats.CategoryPoints);
        _levelCompletePanel.GameStatsPanel.SetTime((int)_globalTime);
        _levelCompletePanel.GameStatsPanel.SetCoins(100);
        
        _currenciesController.Add(Currency.Type.Money, 100);
        
        ResetGlobalTimer();
        ResetTurnTimer();
        ResetSecondChance();
        
        _powerUpsController.SetPowerUpsUsableState(false);

        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.LevelComplete);
    }

    private async UniTaskVoid CompleteTimeChallengeLevelAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_resultViewTime));
        
        CompleteTimeChallengeLevel();
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
        _powerUpsController.SetPowerUpsUsableState(false);
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

        
        if (_secondChancesCount == 0)
            GameOverTimeChallengeAsync();
        else
            ResetSecondChance();
    }

    private void HandlePlayerGameSessionStatsUpdateEvent(PlayerGameSessionStats playerGameSessionStats)
    {
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScore(_playerGameSessionStats.CategoryPoints);
    }
    
    private void HandlePowerUpActivateEvent(PowerUp powerUp)
    {
        if (powerUp.PowerUpType == PowerUp.Type.Answer50)
        {
            List<AnswerUIButton> twoIncorrectAnswersUiButtons = GetTwoRandomIncorrectAnswersUiButtons();

            foreach (AnswerUIButton incorrectAnswerUiButton in twoIncorrectAnswersUiButtons)
            {
                incorrectAnswerUiButton.SetAnswerViewResult(false);
                incorrectAnswerUiButton.SetInteractable(false);
            }

            _turnTimer = 0f;
        }
        
        if (powerUp.PowerUpType == PowerUp.Type.Time)
        {
            _turnTime += 60f;
            _uiController.ItemsPopup.Hide();
        }
        
        if (powerUp.PowerUpType == PowerUp.Type.SecondChance)
        {
            _secondChancesCount = 1;
        }
        
        _uiController.ItemsPopup.Hide();
    }
}