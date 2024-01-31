using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPowerUpUISlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amountText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private GameObject _buyButtonObject;
    [SerializeField] private GameObject _useButtonObject;
    private bool _isUsable;
    private PowerUp _powerUp;

    public event Action<PowerUp> OnBuyButtonClicked;
    public event Action<PowerUp> OnUseButtonClicked;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(InvokeBuyButtonClickEvent);
        _useButton.onClick.AddListener(InvokeUseButtonClickEvent);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(InvokeBuyButtonClickEvent);
        _useButton.onClick.RemoveListener(InvokeUseButtonClickEvent);
    }

    public void SetPowerUpView(PowerUp powerUp)
    {
        _powerUp = powerUp;
        _icon.sprite = powerUp.Icon;
        _amountText.text = powerUp.Amount.ToString();
        _nameText.text = powerUp.PowerUpType.ToString();
        _priceText.text = powerUp.Price.ToString();
    }

    public void SetUsableState(bool isUsable)
    {
        _isUsable = isUsable;

        if (_isUsable)
        {
            _buyButtonObject.gameObject.SetActive(false);
            _useButtonObject.gameObject.SetActive(true);
        }
        else
        {
            _buyButtonObject.gameObject.SetActive(true);
            _useButtonObject.gameObject.SetActive(false);
        }
    }

    public void SetAmount(int amount)
    {
        _amountText.text = amount.ToString();
    }

    private void InvokeBuyButtonClickEvent()
    {
        OnBuyButtonClicked?.Invoke(_powerUp);
    }

    private void InvokeUseButtonClickEvent()
    {
        OnUseButtonClicked?.Invoke(_powerUp);
    }
}