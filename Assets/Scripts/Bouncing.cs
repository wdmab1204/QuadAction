using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Bouncing : MonoBehaviour
{
    private Sequence s;
    void Start()
    {
        s = DOTween.Sequence();
        s.Append(transform.DOScale(new Vector2(1.7f, 1.7f), 0.5f).SetEase(Ease.OutCubic));
        s.Append(transform.DOScale(new Vector2(1.0f, 1.0f), 0.5f).SetEase(Ease.InCubic));
        s.SetAutoKill(false);
        s.SetLoops(-1, LoopType.Yoyo);
    }

    private void OnEnable()
    {
        s.Restart();
    }

}