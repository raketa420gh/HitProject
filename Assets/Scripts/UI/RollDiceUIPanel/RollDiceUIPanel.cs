using System;
using TMPro;
using UnityEngine;

public class RollDiceUIPanel : UIPanel
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _diceFrontSideText;
    [SerializeField] private string _resetText = "ROLL DICE";

    public event Action<QuestionCategoryType> OnRollDiceCompleted;

    public void Reset()
    {
        _diceFrontSideText.text = _resetText;
    }

    public void RollDice()
    {
        _animator.SetTrigger("Roll");
    }

    private void SetDiceFrontSide(QuestionCategoryType questionCategoryType)
    {
        _diceFrontSideText.text = questionCategoryType.ToString();
    }

    private void HandleRollDiceCompleteEvent()
    {
        QuestionCategoryType questionCategoryType = CoreExtensions.ExtendedRandom.RandomEnumValue<QuestionCategoryType>();
        
        SetDiceFrontSide(questionCategoryType);
        
        OnRollDiceCompleted?.Invoke(questionCategoryType);
    }
}