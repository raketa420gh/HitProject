using TMPro;
using UnityEngine;

public class QuestionUIPanel : UIPanel
{
    [SerializeField] private TMP_Text _questionCategoryText;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private AnswerUIButton[] _answerButtons;
    [SerializeField] private UIPanel _trueAnswerPanel;
    [SerializeField] private UIPanel _wrongAnswerPanel;

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
            _trueAnswerPanel.Show();
            _wrongAnswerPanel.Hide();
        }
        else
        {
            _wrongAnswerPanel.Show();
            _trueAnswerPanel.Hide();
        }
    }

    public void HideResultView()
    {
        _trueAnswerPanel.Hide();
        _wrongAnswerPanel.Hide();
    }
}