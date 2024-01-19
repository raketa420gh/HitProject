using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SelectPlayerTurnUIPanel : UIPanel
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _diceFrontSideText;

    public event Action<PlayerTurnType> OnPlayerTurnTypeSelected;

    public void RollDiceToSelectNewPlayerTurnType()
    {
        Reset();
    }

    private void Reset()
    {
        _diceFrontSideText.text = "";
    }

    private async UniTaskVoid SetFirstPlayerTurnType(PlayerTurnType playerTurnType)
    {
        SetDiceFrontSideText(playerTurnType);

        await UniTask.Delay(TimeSpan.FromSeconds(1));
        
        Hide();

        OnPlayerTurnTypeSelected?.Invoke(playerTurnType);
    }

    private void SetDiceFrontSideText(PlayerTurnType playerTurnType)
    {
        _diceFrontSideText.text = playerTurnType.ToString() + " turn!";
    }

    private void HandleRollDiceCompleteEvent()
    {
        PlayerTurnType playerTurnType = CoreExtensions.ExtendedRandom.RandomEnumValue<PlayerTurnType>();

        SetFirstPlayerTurnType(playerTurnType);
    }
}