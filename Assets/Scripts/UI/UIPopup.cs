public class UIPopup : UIPanel
{
    private IUIPanel _backgroundPanel;

    public override void Hide()
    {
        _backgroundPanel?.Show();

        base.Hide();
    }
}