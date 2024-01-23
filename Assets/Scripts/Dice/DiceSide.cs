using TMPro;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    [SerializeField] private QuestionCategoryType _questionCategoryType;
    [SerializeField] private TMP_Text _sideText;

    public QuestionCategoryType QuestionCategoryType => _questionCategoryType;

    private void Awake()
    {
        _sideText.text = _questionCategoryType.ToString();
    }
}