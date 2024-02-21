using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelLose : UIPanel
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private RectTransformMoveAnimation _restartButtonMoveAnimation;

    public event Action OnRestartButtonClicked;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(InvokeRestartButtonClickEvent);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(InvokeRestartButtonClickEvent);
    }

    private void InvokeRestartButtonClickEvent()
    {
        OnRestartButtonClicked?.Invoke();
    }
}