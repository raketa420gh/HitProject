using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectQuestionCategoryUIButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private QuestionCategoryType _questionCategoryType;

    public event Action<QuestionCategoryType> OnSelected;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonClickEvent);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonClickEvent);
    }

    private void HandleButtonClickEvent()
    {
        OnSelected?.Invoke(_questionCategoryType);
    }
}