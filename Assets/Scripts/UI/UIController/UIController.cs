using UnityEngine;

public class UIController : MonoBehaviour, IUIController
{
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private UIPanelHud _hudPanel;
    [SerializeField] private UIPanelLose _losePanel;

    public RectTransform CanvasRect => _canvasRect;
    public UIPanelHud HudPanel => _hudPanel;
    public UIPanelLose LosePanel => _losePanel;
}