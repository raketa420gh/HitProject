using UnityEngine;

public class UIFactory : MonoBehaviour
{
    [SerializeField] private UICurrencyPool _currencyPool;
    [SerializeField] private UIFloatingTextPool _floatingTextPool;
    
    public void Initialize()
    {
        _currencyPool.Initialize();
        _floatingTextPool.Initialize();
    }

    public UIPanelCurrency CreateUICurrency(Transform parent)
    {
        UIPanelCurrency uiCurrency = _currencyPool.GetElement();
        uiCurrency.transform.SetParent(parent);

        return uiCurrency;
    }

    public UIFloatingText CreateFloatingText(Transform parent)
    {
        UIFloatingText floatingText = _floatingTextPool.GetElement();
        floatingText.transform.SetParent(parent);
        
        return floatingText;
    }
}