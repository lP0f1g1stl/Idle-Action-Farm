using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGardenData", menuName = "GDData/GardenObjects/GardenData", order =1)]
public class GardenData : ScriptableObject
{
    [SerializeField] private PlantBlockLogic _plantBlock;
    [Space]
    [SerializeField] private float _growDuration;
    [SerializeField] private float _minScaleY;
    [SerializeField] private float _maxScaleY;

    public PlantBlockLogic PlantBlock => _plantBlock;
    public float GrowDuration => _growDuration;
    public float MinScaleY => _minScaleY;
    public float MaxScaleY => _maxScaleY;
}
