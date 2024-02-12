using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(UIIntValueView))]
[RequireComponent(typeof(CanvasGroup))]

public class UIFloatingText : MonoBehaviour
{
    [SerializeField] private float _floatingTime = 1f;
    [SerializeField] private Vector2 _floatingVelocity = Vector2.up * 100f;
    private RectTransform _rectTransform;
    private UIIntValueView _valueView;
    private CanvasGroup _canvasGroup;
    private Camera _camera;
    private RectTransform _canvasRect;
    private bool _isActive;
    private float _timer;

    private void Awake()
    {
        _camera = Camera.main;
        _valueView = GetComponent<UIIntValueView>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        _timer = 0f;
        _isActive = false;
    }

    private void Update()
    {
        if (!_isActive)
            return;

        _timer += Time.deltaTime;

        _canvasGroup.alpha = 1 - (_timer / _floatingTime);

        if (_timer > _floatingTime)
        {
            gameObject.SetActive(false);
        }
    }

    public void Initialize(RectTransform canvasRect)
    {
        _canvasRect = canvasRect;
        _rectTransform.localScale = Vector3.one;
        _rectTransform.anchoredPosition3D = new Vector3(0,0,0);
    }

    public void ActivateFloatingText(Vector3 worldPosition, int value)
    {
        _valueView.SetText($"+{value.ToString()}");

        Vector2 viewportPosition = ConvertToCanvasPoint(worldPosition);

        Rect rect = _rectTransform.rect;
        _rectTransform.rect.Set(viewportPosition.x, viewportPosition.y, rect.width, rect.height);
        
        _isActive = true;

        _rectTransform.DOAnchorPos(_rectTransform.rect.position + _floatingVelocity, _floatingTime);
    }

    private Vector2 ConvertToCanvasPoint(Vector3 worldPosition)
    {
        Vector2 viewportPosition = _camera.WorldToViewportPoint(worldPosition);
        Vector2 worldObjectScreenPosition = new Vector2
        (
            ((viewportPosition.x * _canvasRect.sizeDelta.x) - (_canvasRect.sizeDelta.x * 0.5f)),
            ((viewportPosition.y * _canvasRect.sizeDelta.y) - (_canvasRect.sizeDelta.y * 0.5f))
        );

        return worldObjectScreenPosition;
    }
}