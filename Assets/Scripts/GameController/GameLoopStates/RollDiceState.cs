using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RollDiceState : GameLoopState
{
    private readonly DicePhysical _dicePhysical;
    private readonly IUIController _uiController;
    private readonly RollDiceUIPanel _rollDicePanel;
    private readonly ILevelController _levelController;
    private QuestionCategoryType _rolledCategoryType = QuestionCategoryType.Triforce;

    public QuestionCategoryType RolledCategoryType => _rolledCategoryType;

    public RollDiceState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _levelController = gameLoopStateMachine.Parent.LevelController;
        _dicePhysical = gameLoopStateMachine.Parent.DicePhysical;
        _uiController = gameLoopStateMachine.Parent.UIController;
        _rollDicePanel = _uiController.RollDicePanel;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _rollDicePanel.OnTriforceCategorySelected += HandleTriforceCategorySelectEvent;
        _rollDicePanel.OnRerollButtonClicked += HandleRerollButtonClickEvent;
        _dicePhysical.OnRollDiceCompleted += HandleRollDicePhysicalCompleteEvent;
        _rollDicePanel.Show();
        
        HandleRerollButtonClickEvent();
    }

    public override void OnStateDisabled()
    {
        _rollDicePanel.Hide();
        _rollDicePanel.OnTriforceCategorySelected -= HandleTriforceCategorySelectEvent;
        _rollDicePanel.OnRerollButtonClicked -= HandleRerollButtonClickEvent;
        _dicePhysical.OnRollDiceCompleted -= HandleRollDicePhysicalCompleteEvent;
    }

    public override void Update()
    {
        
    }
    
    private async UniTaskVoid SetRolledDiceAsync(QuestionCategoryType questionCategoryType)
    {
        if (questionCategoryType == QuestionCategoryType.Triforce)
        {
            _rollDicePanel.DisableButtons();
            
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            
            _rollDicePanel.EnableSelectCategoryPanel();
            
            return;
        }
        
        _rollDicePanel.DisableSelectCategoryPanel();
        _rollDicePanel.EnableButtons();

        _rolledCategoryType = questionCategoryType;
    }

    private void HandleTriforceCategorySelectEvent(QuestionCategoryType questionCategoryType)
    {
        _rolledCategoryType = questionCategoryType;
        _levelController.HandleRollDicePlayButtonEvent();
    }

    private void HandleRerollButtonClickEvent()
    {
        _rollDicePanel.DisableButtons();
        _dicePhysical.Reroll();
    }

    private void HandleRollDicePhysicalCompleteEvent(QuestionCategoryType questionCategoryType)
    {
        SetRolledDiceAsync(questionCategoryType);
    }
}