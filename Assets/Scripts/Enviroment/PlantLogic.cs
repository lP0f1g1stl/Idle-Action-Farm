using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlantLogic : MonoBehaviour
{
    [SerializeField] private Transform _plants;
    [SerializeField] private Transform _spawnPoint;
    [Space]
    [SerializeField] private GardenData _gardenData;
    [Space]
    [SerializeField] private ParticleSystem _particleSystem;
    

    private bool _isReady;

    private Tween _growTween;

    private void Start()
    {
        Grow();
    }
    private void Grow() 
    {
        _growTween = _plants.DOScaleY(_gardenData.MaxScaleY, _gardenData.GrowDuration);
        _growTween.OnComplete(() => _isReady = true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isReady && other.gameObject.GetComponent<Sickle>())
        {
            Cut();
            Instantiate(_gardenData.PlantBlock, _spawnPoint);
        }
    }
    private void Cut() 
    {
        _particleSystem.Play();
        _isReady = false;
        _plants.localScale = new Vector3(1, _gardenData.MinScaleY, 1);
        Grow();
    }
}
