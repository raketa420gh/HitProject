using UnityEngine;
using UnityEngine.UI;

public class QuizSession : MonoBehaviour
{
    [SerializeField] private QuestionData[] _questions;
    [SerializeField] private Text _questionText;
    [SerializeField] private Button[] _answerButtons;

    private int _currentQuestionIndex;

    public void StartSession(QuestionCategoryType categoryType)
    {
        SetCurrentQuestion(0);
        DisplayCurrentQuestion();
    }

    private void SetCurrentQuestion(int index)
    {
        _currentQuestionIndex = index;
    }

    private void DisplayCurrentQuestion()
    {
        int questionIndex = _currentQuestionIndex;
        
        QuestionData question = _questions[questionIndex];
        _questionText.text = question.Question;

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            Button button = _answerButtons[i];
            button.GetComponentInChildren<Text>().text = question.Answers[i];
            int answerIndex = i;
            button.onClick.AddListener(() => CheckAnswer(answerIndex, question.CorrectAnswerIndex));
        }
    }

    private void CheckAnswer(int selectedAnswerIndex, int correctAnswerIndex)
    {
        if (selectedAnswerIndex == correctAnswerIndex)
        {
            Debug.Log("Correct answer!");
            // Handle correct answer logic here
        }
        else
        {
            Debug.Log("Wrong answer!");
            // Handle wrong answer logic here
        }

        SetNextQuestion();
    }

    private void SetNextQuestion()
    {
        _currentQuestionIndex++;
        
        if (_currentQuestionIndex < _questions.Length)
        {
            DisplayCurrentQuestion();
        }
        else
        {
            // Handle end of the quiz logic here
        }
    }
}