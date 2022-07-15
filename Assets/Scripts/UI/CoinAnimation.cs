using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CoinAnimation : MonoBehaviour
{
    private Tween _animation;

    public event Action OnAnimationComplete;
    public void AnimateCoin(Vector3 startPos, Transform endPos, float duration) 
    {
        gameObject.transform.position = startPos;
        gameObject.SetActive(true);
        _animation = transform.DOMove(endPos.position, duration);
        _animation.OnComplete(() => {
            OnAnimationComplete?.Invoke();
            gameObject.SetActive(false);
            });
    }
}
