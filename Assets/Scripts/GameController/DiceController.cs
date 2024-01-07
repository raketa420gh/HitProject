using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] private Dice _dice;
    
    public void RollDice()
    {
        
    }
    
    private QuestionCategoryType GetRandomQuestionCategoryType()
    {
        return CoreExtensions.ExtendedRandom.RandomEnumValue<QuestionCategoryType>();
    }
}