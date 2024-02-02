using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]

public class UISafeAreaFitter : MonoBehaviour
{
    private RectTransform _safeAreaTransform;

    private void Awake()
    {
        _safeAreaTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        UpdateSafeArea(Screen.safeArea, new Vector2(Screen.width, Screen.height));
    }

    private void UpdateSafeArea(Rect _safeArea, Vector2 _screen)
    {
        Vector2 saAnchorMin;
        Vector2 saAnchorMax;
        saAnchorMin.x = _safeArea.x / _screen.x;
        saAnchorMin.y = _safeArea.y / _screen.y;
        saAnchorMax.x = (_safeArea.x + _safeArea.width) / _screen.x;
        saAnchorMax.y = (_safeArea.y + _safeArea.height) / _screen.y;
        
        _safeAreaTransform.anchorMin = saAnchorMin;
        _safeAreaTransform.anchorMax = saAnchorMax;
    }
}
