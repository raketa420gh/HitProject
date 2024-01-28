using System;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class CreatePlayerUIPanel : UIPanel
{
    [SerializeField] private Button _confirmNameButton;
    [SerializeField] private TMP_InputField _nameInputField;

    public event Action<string> OnNameConfirmButtonClicked;

    private void OnEnable()
    {
        _confirmNameButton.onClick.AddListener(HandleConfirmNameButtonEvent);
    }

    private void OnDisable()
    {
        _confirmNameButton.onClick.RemoveListener(HandleConfirmNameButtonEvent);
    }

    private void HandleConfirmNameButtonEvent()
    {
        OnNameConfirmButtonClicked?.Invoke(_nameInputField.text);
    }
}