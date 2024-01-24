using System;
using UnityEngine;
using UnityEngine.UI;

public class RollDiceUIPanel : UIPanel
{
    [SerializeField] private Button _rollDiceButton;
    [SerializeField] private UIPanel _buttonsPanel;
    [SerializeField] private string _resetText = "ROLL DICE";
    [SerializeField] private SelectQuestionCategoryUIPanel _selectCategoryPanel;

    public event Action<QuestionCategoryType> OnTriforceCategorySelected;
    public event Action OnRerollButtonClicked;

    private void OnEnable()
    {
        Reset();
        _selectCategoryPanel.OnQuestionCategorySelected += HandleQuestionCategorySelectEvent;
    }

    private void OnDisable()
    {
        _selectCategoryPanel.OnQuestionCategorySelected -= HandleQuestionCategorySelectEvent;
    }

    public void Reset()
    {
        _buttonsPanel.Hide();
    }

    public void ActivateExtraSpin()
    {
        Reset();
        
        OnRerollButtonClicked?.Invoke();
    }

    public void EnableButtons()
    {
        _buttonsPanel.Show();
    }

    public void DisableButtons()
    {
        _buttonsPanel.Hide();
    }

    public void EnableSelectCategoryPanel()
    {
        _selectCategoryPanel.Show();
    }
    
    public void DisableSelectCategoryPanel()
    {
        _selectCategoryPanel.Hide();
    }

    private void HandleQuestionCategorySelectEvent(QuestionCategoryType questionCategoryType)
    {
        _selectCategoryPanel.Hide();
        _buttonsPanel.Show();

        OnTriforceCategorySelected?.Invoke(questionCategoryType);
    }
}