using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPowerUpUISlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amountText;

    public void SetPowerUpView(PowerUp powerUp)
    {
        _icon.sprite = powerUp.Icon;
        _amountText.text = powerUp.Amount.ToString();
    }
}