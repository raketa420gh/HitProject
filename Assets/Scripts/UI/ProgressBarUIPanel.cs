using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUIPanel : UIPanel
{
    [SerializeField] private Image _fillImage;

    public void Reset()
    {
        _fillImage.fillAmount = 1;
    }

    public void SetFillAmount(float normalizedAmount)
    {
        _fillImage.fillAmount = normalizedAmount;
    }
}