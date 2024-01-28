using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class CreatePlayerUIPanel : UIPanel
{
    [SerializeField] private Button _confirmNameButton;
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private Button _openIconSelectButton;
    [SerializeField] private Image _mainIconImage;
    [SerializeField] private SelectIconUIPanel _selectIconPanel;

    public SelectIconUIPanel SelectIconPanel => _selectIconPanel;

    public event Action<string> OnNameConfirmButtonClicked;
    public event Action<Sprite> OnIconSelected;

    private void OnEnable()
    {
        _confirmNameButton.onClick.AddListener(HandleConfirmNameButtonEvent);
        _openIconSelectButton.onClick.AddListener(HandleOpenSelectIconPanelEvent);
        _selectIconPanel.OnIconSelected += HandleSelectIconEvent;
    }

    private void OnDisable()
    {
        _confirmNameButton.onClick.RemoveListener(HandleConfirmNameButtonEvent);
        _openIconSelectButton.onClick.RemoveListener(HandleOpenSelectIconPanelEvent);
        _selectIconPanel.OnIconSelected -= HandleSelectIconEvent;
    }

    private void HandleConfirmNameButtonEvent()
    {
        OnNameConfirmButtonClicked?.Invoke(_nameInputField.text);
    }
    
    private void HandleSelectIconEvent(IconUISlot iconSlot)
    {
        _mainIconImage.sprite = iconSlot.Sprite;
        
        OnIconSelected?.Invoke(iconSlot.Sprite);
    }
    
    private void HandleOpenSelectIconPanelEvent()
    {
        _selectIconPanel.Show();
    }
}