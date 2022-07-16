using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantBlockAnimationData", menuName = "GDData/GardenObjects/PlantBlockAnimationData", order = 2)]
public class PlantBlockAnimationData : ScriptableObject
{
    [SerializeField] private Vector3 _dropRadius;
    [Space]
    [SerializeField] private float _scaleMultiplier;
    [SerializeField] private float _animationDuration;

    public Vector3 DropRadius => _dropRadius;
    public float ScaleMultiplier => _scaleMultiplier;
    public float AnimationDuration => _animationDuration;
}
