using System.Collections.Generic;
using UnityEngine;

public class SoloGameState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly IUIController _uiController;
    private readonly InGameUIPanel _inGamePanel;
    private readonly AnswerUIButton[] _answerUIButtons;
    private List<QuestionData> _categoryQuestions = new List<QuestionData>();
    private int _currentCorrectAnswerIndex;
    private bool _isResultViewing;
    private float _resultViewTimer;
    private float _resultViewTime = 2f;

    public SoloGameState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _uiController = gameLoopStateMachine.Parent.UIController;
        _inGamePanel = _uiController.InGamePanel;
        _answerUIButtons = _inGamePanel.QuestionPanel.AnswerUIButtons;
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
        
        InitializeQuestionsCategory();
        ActivateNextQuestion();
        _inGamePanel.Show();
    }

    public override void OnStateDisabled()
    {
        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.OnClicked -= HandleAnswerClickEvent;
    }

    public override void Update()
    {
        if (_isResultViewing)
        {
            _resultViewTimer += Time.deltaTime;

            if (_resultViewTimer > _resultViewTime)
            {
                _resultViewTimer = 0f;
                _isResultViewing = false;
                
                ActivateNextQuestion();
            }
        }
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
    }

    private void HandleAnswerClickEvent(int index)
    {
        AnswerUIButton clickedAnswerButton = _answerUIButtons[index];
        AnswerUIButton correctAnswerButton = _answerUIButtons[_currentCorrectAnswerIndex];
        
        foreach (AnswerUIButton answerUIButton in _answerUIButtons)
            answerUIButton.SetInteractable(false);

        if (index == _currentCorrectAnswerIndex)
        {
            Debug.Log("CORRECT ANSWER");
            
            _inGamePanel.QuestionPanel.ShowResultView(true);
            _answerUIButtons[index].SetAnswerViewResult(true);
        }
        else
        {
            Debug.Log("WRONG ANSWER");
            
            _inGamePanel.QuestionPanel.ShowResultView(false);
            _answerUIButtons[index].SetAnswerViewResult(false);
            _answerUIButtons[_currentCorrectAnswerIndex].SetAnswerViewResult(true);
        }
        
        _isResultViewing = true;
    }
}