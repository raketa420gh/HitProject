using UnityEngine;

public class UIFloatingTextPool : MonoBehaviour
{
    [SerializeField] private int _poolCount = 10;
    [SerializeField] private bool _isAutoExpand = true;
    [SerializeField] private UIFloatingText _prefab;

    private Pool<UIFloatingText> _pool;

    public void Initialize()
    {
        _pool = new Pool<UIFloatingText>(_prefab, _poolCount, transform)
        {
            AutoExpand = _isAutoExpand
        };
    }

    public UIFloatingText GetElement()
    {
        return _pool.GetFreeElement();
    }
}