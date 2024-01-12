using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StarsUIPanel : UIPanel
{
    [SerializeField] private Image[] _stars;
    
    public event Action OnStarsAnimationCompleted;
    
    public async UniTaskVoid ShowStarsAnimation()
    {
        ResetStars();

        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].transform.DOScale(Vector3.one, 1f);

            await Task.Delay(TimeSpan.FromSeconds(1f));
        }
        
        OnStarsAnimationCompleted?.Invoke();
    }

    private void ResetStars()
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].transform.localScale = Vector3.zero;
        }
    }
}