using UnityEngine;

public class MainMenuUIPanel : UIPanel
{
    [SerializeField] private GameObject _startButtonObject;

    public void SetStartButtonObjectView(bool isActive)
    {
        _startButtonObject.SetActive(isActive);
    }
}