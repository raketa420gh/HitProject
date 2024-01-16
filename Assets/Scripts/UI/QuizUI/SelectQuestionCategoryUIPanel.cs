using System;
using UnityEngine;

public class SelectQuestionCategoryUIPanel : UIPanel
{
    [SerializeField] private SelectQuestionCategoryUIButton[] _selectCategoryButtons;
    
    public event Action<QuestionCategoryType> OnQuestionCategorySelected;

    private void OnEnable()
    {
        foreach (SelectQuestionCategoryUIButton selectCategoryButton in _selectCategoryButtons)
            selectCategoryButton.OnSelected += HandleSelectCategoryEvent;
    }

    private void OnDisable()
    {
        foreach (SelectQuestionCategoryUIButton selectCategoryButton in _selectCategoryButtons)
            selectCategoryButton.OnSelected -= HandleSelectCategoryEvent;
    }

    private void HandleSelectCategoryEvent(QuestionCategoryType questionCategoryType)
    {
        OnQuestionCategorySelected?.Invoke(questionCategoryType);
    }
}