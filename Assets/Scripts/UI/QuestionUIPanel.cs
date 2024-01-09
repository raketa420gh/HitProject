using TMPro;
using UnityEngine;

public class QuestionUIPanel : UIPanel
{
    [SerializeField] private TMP_Text _questionCategoryText;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private AnswerUIButton[] _answerButtons;

    public AnswerUIButton[] AnswerUIButtons => _answerButtons;

    public void SetQuestion(QuestionData questionData)
    {
        _questionCategoryText.text = questionData.CategoryType.ToString();
        _questionText.text = questionData.Question;
        
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _answerButtons[i].SetText(questionData.Answers[i]);
            _answerButtons[i].SetIndex(i);
        }
    }

    public void ShowResultView(bool isCorrect)
    {
        if (isCorrect)
        {
            _questionText.text = "EXCELLENT!";
        }
        else
        {
            _questionText.text = "INCORRECT";
        }
    }
}