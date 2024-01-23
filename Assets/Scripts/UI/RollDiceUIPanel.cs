using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RollDiceUIPanel : UIPanel
{
    [SerializeField] private GameObject _physicalDiceParent;
    [SerializeField] private Button _rollDiceButton;
    [SerializeField] private UIPanel _buttonsPanel;
    [SerializeField] private string _resetText = "ROLL DICE";
    [SerializeField] private SelectQuestionCategoryUIPanel _selectCategoryPanel;

    public event Action<QuestionCategoryType> OnRollDiceCompleted;
    public event Action OnRollDiceStarted;

    private void OnEnable()
    {
        _selectCategoryPanel.OnQuestionCategorySelected += HandleQuestionCategorySelectEvent;
        
        _physicalDiceParent.SetActive(true);
    }

    private void OnDisable()
    {
        _selectCategoryPanel.OnQuestionCategorySelected -= HandleQuestionCategorySelectEvent;
        
        _physicalDiceParent.SetActive(false);
    }

    public void Reset()
    {
        _buttonsPanel.Hide();
    }

    public void ActivateExtraSpin()
    {
        Reset();
        //RollDice();
    }

    private async UniTaskVoid SetRolledDice(QuestionCategoryType questionCategoryType)
    {
        if (questionCategoryType == QuestionCategoryType.Triforce)
        {
            _buttonsPanel.Hide();
            
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            
            _selectCategoryPanel.Show();
            
            return;
        }
        
        _selectCategoryPanel.Hide();
        _buttonsPanel.Show();

        OnRollDiceCompleted?.Invoke(questionCategoryType);
    }

    private void HandleQuestionCategorySelectEvent(QuestionCategoryType questionCategoryType)
    {
        SetRolledDice(questionCategoryType);
    }

    private void HandleRollDiceCompleteEvent()
    {
        QuestionCategoryType questionCategoryType = CoreExtensions.ExtendedRandom.RandomEnumValue<QuestionCategoryType>();
        
        _selectCategoryPanel.Hide();
        SetRolledDice(questionCategoryType);
    }
}