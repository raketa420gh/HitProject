using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoloGameState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly IUIController _uiController;
    private readonly InGameUIPanel _inGamePanel;
    private readonly LevelCompleteUIPanel _levelCompletePanel;
    private readonly GameOverUIPanel _gameOverPanel;
    private readonly SoloGameStageCompleteUIPanel _soloGameStageCompletePanel;
    private readonly AnswerUIButton[] _answerUIButtons;
    private readonly ILevelController _levelController;
    private readonly ICurrenciesController _currenciesController;
    private readonly IPowerUpsController _powerUpsController;
    private readonly ParallaxController _parallaxController;
    private List<QuestionData> _categoryQuestions = new List<QuestionData>();
    private int _currentCorrectAnswerIndex;
    private int _activeLevelNumber;
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
    private int _secondChancesCount;
    
    public SoloGameState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _uiController = _gameLoopStateMachine.Parent.UIController;
        _inGamePanel = _uiController.InGamePanel;
        _levelCompletePanel = _uiController.LevelCompletePanel;
        _gameOverPanel = _uiController.GameOverPanel;
        _soloGameStageCompletePanel = _uiController.SoloGameStageCompletePanel;
        _answerUIButtons = _inGamePanel.QuestionPanel.AnswerUIButtons;
        _levelController = _gameLoopStateMachine.Parent.LevelController;
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

        _levelController.SetGameMode(GameModeType.Solo);
        _inGamePanel.TurnTimeProgressBar.Show();
        _inGamePanel.GlobalTimeProgressBar.Hide();
        _isGlobalTimerActive = true;

        _parallaxController.EnableParallax();
        ResetResultViewTimer();
        InitializePlayerSession();
        InitializeQuestionsCategory();
        ActivateNextQuestion();

        _inGamePanel.Show();
    }

    public override void OnStateDisabled()
    {
        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.OnClicked -= HandleAnswerClickEvent;

        _playerGameSessionStats.OnUpdated -= HandlePlayerGameSessionStatsUpdateEvent;
        _powerUpsController.OnPowerUpActivated -= HandlePowerUpActivateEvent;

        _parallaxController.ResetPositions();
        _parallaxController.DisableParallax();

        _isGlobalTimerActive = false;

        _inGamePanel.Hide();
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

        if (_isTurnTimerActive)
        {
            _turnTimer += Time.deltaTime;

            _turnTimeProgressNormalized = _turnTimer / _turnTime;
            _inGamePanel.TurnTimeProgressBar.SetFillAmount(_turnTimeProgressNormalized);

            if (_turnTimer > _turnTime)
            {
                GameOverSolo();
            }
        }

        if (_isGlobalTimerActive)
        {
            _globalTimer += Time.deltaTime;
        }
    }

    public void SetLevel(int levelNumber)
    {
        _activeLevelNumber = levelNumber;
    }

    private void GameOverSolo()
    {
        _gameOverPanel.GameStatsPanel.SetScore(_playerGameSessionStats.CategoryPoints);
        _gameOverPanel.GameStatsPanel.SetTime((int)_globalTimer);
        _gameOverPanel.GameStatsPanel.SetCoins(0);

        _playerGameSessionStats.ResetAll();
        ResetTurnTimer();
        ResetGlobalTimer();
        ResetSecondChance();
        ResetResultViewTimer();

        _powerUpsController.SetPowerUpsUsableState(false);

        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.GameOver);
    }

    private void LevelCompleteSolo()
    {
        _levelController.CompleteLevel(_activeLevelNumber);

        _levelCompletePanel.GameStatsPanel.SetScore(_playerGameSessionStats.CategoryPoints);
        _levelCompletePanel.GameStatsPanel.SetTime((int)_globalTimer);
        _levelCompletePanel.GameStatsPanel.SetCoins(100);

        _currenciesController.Add(Currency.Type.Money, 100);

        _playerGameSessionStats.ResetAll();
        ResetTurnTimer();
        ResetGlobalTimer();
        ResetSecondChance();

        _powerUpsController.SetPowerUpsUsableState(false);

        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.LevelComplete);
    }

    private void ResetResultViewTimer()
    {
        _resultViewTimer = 0f;
        _isResultViewing = false;
    }

    private void ResetTurnTimer()
    {
        _turnTimer = 0f;
        _isTurnTimerActive = false;
    }

    private void ResetGlobalTimer()
    {
        _globalTimer = 0f;
        _isGlobalTimerActive = false;
    }

    private void InitializePlayerSession()
    {
        _playerGameSessionStats ??= new PlayerGameSessionStats();
        _playerGameSessionStats.OnUpdated += HandlePlayerGameSessionStatsUpdateEvent;
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScoreView(true);
    }

    private void InitializeQuestionsCategory()
    {
        _categoryQuestions = new List<QuestionData>();
        RollDiceState rollDiceState =
            (RollDiceState)_gameLoopStateMachine.GetState(GameLoopStateMachine.State.RollDice);

        List<QuestionData> questionsDatabase = _gameLoopStateMachine.Parent.QuestionsDatabase;

        for (int i = 0; i < questionsDatabase.Count; i++)
        {
            QuestionData questionData = questionsDatabase[i];

            if (questionData.CategoryType == rollDiceState.RolledCategoryType)
            {
                _categoryQuestions.Add(questionData);
            }
        }

        Debug.Log(
            $"Questions initialized. Category = {rollDiceState.RolledCategoryType}, Questions count = {_categoryQuestions.Count}");
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

        TweenerCore<Vector2, Vector2, VectorOptions> tween =
            questionPanelRect.DOAnchorPosX(questionPanelRect.anchoredPosition.x + distanceToMove, animationTime);

        tween.onComplete += ActivateNextQuestion;
    }

    private void ActivateNextQuestion()
    {
        _inGamePanel.QuestionPanel.CurrentQuestionPanelRect.DOAnchorPosX(0, 0);
        _isTurnTimerActive = true;
        _turnTime = 5f;

        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.Reset();

        int rQuestionDataIndex = Random.Range(0, _categoryQuestions.Count);
        QuestionData currentQuestionData = _categoryQuestions[rQuestionDataIndex];

        _currentCorrectAnswerIndex = currentQuestionData.CorrectAnswerIndex;
        _inGamePanel.QuestionPanel.SetQuestion(currentQuestionData);

        _categoryQuestions.Remove(currentQuestionData);

        if (_categoryQuestions.Count == 0)
        {
            Debug.Log("Reset category questions");
            InitializeQuestionsCategory();
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

    private void ResetSecondChance()
    {
        _secondChancesCount = 0;
    }

    private void HandleAnswerClickEvent(int index)
    {
        AnswerUIButton clickedAnswerButton = _answerUIButtons[index];
        AnswerUIButton correctAnswerButton = _answerUIButtons[_currentCorrectAnswerIndex];

        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.SetInteractable(false);

        clickedAnswerButton.transform.localScale = Vector3.one * 1.1f;
        correctAnswerButton.transform.localScale = Vector3.one * 1.1f;

        _isResultViewing = true;

        ResetTurnTimer();
        _powerUpsController.SetPowerUpsUsableState(false);

        if (index == _currentCorrectAnswerIndex)
            HandleCorrectAnswerAsync(index);
        else
            HandleWrongAnswerAsync(index);
    }

    private void HandlePlayerGameSessionStatsUpdateEvent(PlayerGameSessionStats playerGameSessionStats)
    {
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScore(_playerGameSessionStats.CategoryPoints);
    }

    private async UniTaskVoid HandleCorrectAnswerAsync(int index)
    {
        Debug.Log("CORRECT ANSWER");

        _inGamePanel.QuestionPanel.ShowResultView(true);
        _answerUIButtons[index].SetAnswerViewResult(true);

        _playerGameSessionStats.AddTrueAnswer();

        if (_playerGameSessionStats.TrueAnswersCount >= 3)
        {
            _playerGameSessionStats.ResetTrueAnswers();
            _playerGameSessionStats.AddCategoryPoint();

            if (_playerGameSessionStats.CategoryPoints >= 5)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_resultViewTime));

                LevelCompleteSolo();
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_resultViewTime));

                _soloGameStageCompletePanel.Show();
                await UniTask.Delay(TimeSpan.FromSeconds(_resultViewTime));
                _soloGameStageCompletePanel.Hide();

                _gameLoopStateMachine.SetState(GameLoopStateMachine.State.RollDice);
            }
        }
    }

    private async UniTaskVoid HandleWrongAnswerAsync(int index)
    {
        Debug.Log("WRONG ANSWER");
        
        _inGamePanel.QuestionPanel.ShowResultView(false);
        _answerUIButtons[index].SetAnswerViewResult(false);
        _answerUIButtons[_currentCorrectAnswerIndex].SetAnswerViewResult(true);
        
        await UniTask.Delay(TimeSpan.FromSeconds(_resultViewTime));
        
        if (_secondChancesCount == 0)
            GameOverSolo();
        else
            ResetSecondChance();
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