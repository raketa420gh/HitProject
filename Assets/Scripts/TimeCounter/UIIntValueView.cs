using TMPro;
using UnityEngine;

public class UIIntValueView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SetValueView(int value)
    {
        _text.text = value.ToString();
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}