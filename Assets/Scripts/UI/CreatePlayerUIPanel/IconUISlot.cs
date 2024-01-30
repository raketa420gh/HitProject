using System;
using UnityEngine;
using UnityEngine.UI;

public class IconUISlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    private int _iconNumber;

    public Sprite Sprite => _image.sprite;
    public int IconNumber => _iconNumber;

    public event Action<IconUISlot> OnSelectIconButtonClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonClickEvent);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonClickEvent);
    }

    public void SetNumber(int number)
    {
        _iconNumber = number;
    }

    private void HandleButtonClickEvent()
    {
        OnSelectIconButtonClicked?.Invoke(this);
    }
}