using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private TMP_Text _diceText;

    public void SetText(string text)
    {
        _diceText.text = text;
    }
}