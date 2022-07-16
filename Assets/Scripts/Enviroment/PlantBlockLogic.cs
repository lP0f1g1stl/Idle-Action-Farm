using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlantBlockLogic : MonoBehaviour
{
    [SerializeField] private PlantBlockAnimationData _animationData;

    private Vector3 _defaultScale;
    private Vector3 _changedScale;

    private Tween _animationTween;
    private Tween _scaleTween;

    public event Action OnAnimationComplete;

    private void Awake()
    {
        _defaultScale = transform.localScale;
        _changedScale = transform.localScale * _animationData.ScaleMultiplier;
    }
    private void OnEnable()
    {
        Vector3 dropPoint = new Vector3(UnityEngine.Random.Range(-_animationData.DropRadius.x, _animationData.DropRadius.x), _animationData.DropRadius.y, UnityEngine.Random.Range(-_animationData.DropRadius.z, _animationData.DropRadius.z));
        DropAnimation(dropPoint);
    }
    private void DropAnimation(Vector3 dropPoint) 
    {
        _animationTween = transform.DOLocalMove(dropPoint, _animationData.AnimationDuration).SetEase(Ease.OutBounce);
    }

    public void CollectAnimation(Transform parent, Vector3 pos) 
    {
        _animationTween?.Kill();
        transform.SetParent(parent);
        _animationTween = transform.DOLocalMove(new Vector3(_changedScale.x * pos.x, _changedScale.y * pos.y, 0), _animationData.AnimationDuration);
        _animationTween.OnComplete(() => 
        {
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            OnAnimationComplete?.Invoke();
            });
    }

    public void ChangeScale() 
    {
        transform.DOScale(_changedScale, _animationData.AnimationDuration);
    }
    public void RevertScale() 
    {
        transform.DOScale(_defaultScale, _animationData.AnimationDuration);
    }
}
