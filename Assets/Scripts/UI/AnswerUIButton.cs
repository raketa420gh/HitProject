using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerUIButton : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _answerText;
    private int _answerIndex;

    public int Index => _answerIndex;
    public event Action<int> OnClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClickEvent);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClickEvent);
    }

    public void SetInteractable(bool isActive)
    {
        _button.interactable = isActive;
    }

    public void SetText(string answerText)
    {
        _answerText.text = answerText;
    }

    public void SetIndex(int index)
    {
        _answerIndex = index;
    }

    public void SetAnswerViewResult(bool isTrue)
    {
        _buttonImage.color = isTrue ? Color.green : Color.red;
    }

    public void Reset()
    {
        _answerText.text = "";
        _buttonImage.color = Color.white;
        transform.localScale = Vector3.one;
        SetInteractable(true);
    }

    private void HandleClickEvent()
    {
        OnClicked?.Invoke(_answerIndex);
    }
}