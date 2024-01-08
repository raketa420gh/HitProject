using TMPro;
using UnityEngine;

public class RollDiceUIPanel : UIPanel
{
    [SerializeField] private TMP_Text _diceFrontSideText;

    public void SetDiceFrontSide(QuestionCategoryType questionCategoryType)
    {
        _diceFrontSideText.text = questionCategoryType.ToString();
    }
}