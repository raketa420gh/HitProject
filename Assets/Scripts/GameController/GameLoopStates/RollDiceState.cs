using UnityEngine;

public class RollDiceState : GameLoopState
{
    private readonly IUIController _uiController;
    private readonly RollDiceUIPanel _rollDicePanel;
    private QuestionCategoryType _rolledCategoryType = QuestionCategoryType.Triforce;

    public RollDiceState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
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
        
        _rollDicePanel.Show();
        _rollDicePanel.OnRollDiceCompleted += HandleRollDiceCompleteEvent;
    }

    public override void OnStateDisabled()
    {
        _rollDicePanel.Hide();
        _rollDicePanel.OnRollDiceCompleted -= HandleRollDiceCompleteEvent;
    }

    public override void Update()
    {
        
    }

    private void HandleRollDiceCompleteEvent(QuestionCategoryType questionCategoryType)
    {
        Debug.Log($"DICE = {questionCategoryType}");
    }
}