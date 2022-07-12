using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlantBlockLogic : MonoBehaviour
{
    [SerializeField] private PlantBlockAnimationData _animationData;

    private Tween _animationTween;
    private void OnEnable()
    {
        Vector3 dropPoint = new Vector3(Random.Range(-_animationData.DropRadius.x, _animationData.DropRadius.x), _animationData.DropRadius.y, Random.Range(-_animationData.DropRadius.z, _animationData.DropRadius.z));
        DropAnimation(dropPoint);
    }
    private void DropAnimation(Vector3 dropPoint) 
    {
        _animationTween = transform.DOLocalMove(dropPoint, _animationData.dropDuration).SetEase(Ease.OutBounce);
    }

    public void CollectAnimation(Transform parent, int count) 
    {
        _animationTween?.Kill();
        transform.SetParent(parent);
        _animationTween = transform.DOLocalMove(new Vector3(0, count * transform.localScale.y, 0), _animationData.dropDuration);
        _animationTween.OnComplete(() => transform.localRotation = Quaternion.Euler(Vector3.zero));
    }
}
