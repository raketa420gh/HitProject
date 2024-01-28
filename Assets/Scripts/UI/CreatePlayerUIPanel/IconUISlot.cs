using System;
using UnityEngine;
using UnityEngine.UI;

public class IconUISlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public Sprite Sprite => _image.sprite;

    public event Action<IconUISlot> OnSelectIconButtonClicked;

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
        OnSelectIconButtonClicked?.Invoke(this);
    }
}