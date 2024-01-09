using System;
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

    public event Action<QuestionCategoryType> OnRollDiceCompleted;

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
        _rollDiceButton.interactable = true;
    }

    private void SetDiceFrontSide(QuestionCategoryType questionCategoryType)
    {
        _diceFrontSideText.text = questionCategoryType.ToString();
    }

    private void HandleRollDiceCompleteEvent()
    {
        QuestionCategoryType questionCategoryType = CoreExtensions.ExtendedRandom.RandomEnumValue<QuestionCategoryType>();
        
        SetDiceFrontSide(questionCategoryType);
        _buttonsPanel.Show();

        OnRollDiceCompleted?.Invoke(questionCategoryType);
    }
}