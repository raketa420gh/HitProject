using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollDiceUIPanel : UIPanel
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _rollDiceButton;
    [SerializeField] private UIPanel _buttonsPanel;
    [SerializeField] private TMP_Text _diceFrontSideText;
    [SerializeField] private string _resetText = "ROLL DICE";
    [SerializeField] private SelectQuestionCategoryUIPanel _selectCategoryPanel;

    public event Action<QuestionCategoryType> OnRollDiceCompleted;

    private void OnEnable()
    {
        _selectCategoryPanel.OnQuestionCategorySelected += HandleQuestionCategorySelectEvent;
    }

    private void OnDisable()
    {
        _selectCategoryPanel.OnQuestionCategorySelected -= HandleQuestionCategorySelectEvent;
    }

    public void Reset()
    {
        _diceFrontSideText.text = _resetText;
        _buttonsPanel.Hide();
        _rollDiceButton.interactable = true;
    }

    public void RollDice()
    {
        _animator.SetTrigger("Roll");
        _rollDiceButton.interactable = false;
    }

    public void ActivateExtraSpin()
    {
        Reset();
        RollDice();
    }

    public async UniTaskVoid SetRolledDice(QuestionCategoryType questionCategoryType)
    {
        SetDiceFrontSide(questionCategoryType);

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

    private void SetDiceFrontSide(QuestionCategoryType questionCategoryType)
    {
        _diceFrontSideText.text = questionCategoryType.ToString();
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