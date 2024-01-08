using TMPro;
using UnityEngine;

public class QuestionUIPanel : UIPanel
{
    [SerializeField] private TMP_Text _questionCategoryText;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private AnswerUIButton[] _answerButtons;
}