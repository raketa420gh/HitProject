using UnityEngine;
using UnityEngine.UI;

public class AnswerUIButton : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Button _button;

    public void SetInteractable(bool isActive)
    {
        _button.interactable = isActive;
    }

    public void SetAnswerViewResult(bool isTrue)
    {
        _buttonImage.color = isTrue ? Color.green : Color.red;
    }

    public void Reset()
    {
        _buttonImage.color = Color.white;
        SetInteractable(true);
    }
}