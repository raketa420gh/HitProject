using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIPanel : UIPanel
{
    [SerializeField] private GameObject _startButtonObject;
    [SerializeField] private Button[] _bottonPanelButtons;

    public void SetStartButtonObjectView(bool isActive)
    {
        _startButtonObject.SetActive(isActive);
    }

    public void ShowOnlyItemsButton()
    {
        foreach (Button button in _bottonPanelButtons)
            button.gameObject.SetActive(false);

        _bottonPanelButtons[1].gameObject.SetActive(true);
    }

    public void ShowAllButtons()
    {
        foreach (Button button in _bottonPanelButtons)
            button.gameObject.SetActive(true);
    }
}