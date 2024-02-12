using TMPro;
using UnityEngine;

public class UIIntValueView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    public void SetValueView(int value)
    {
        _timerText.text = value.ToString();
    }
}