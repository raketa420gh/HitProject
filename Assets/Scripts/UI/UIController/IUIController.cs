using UnityEngine;

public interface IUIController
{
    RectTransform CanvasRect { get; }
    UIPanelHud HudPanel { get; }
    UIPanelLose LosePanel { get; }
}