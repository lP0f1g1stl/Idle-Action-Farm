using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantBlockAnimationData", menuName = "GDData/GardenObjects/PlantBlockAnimationData", order = 2)]
public class PlantBlockAnimationData : ScriptableObject
{
    [SerializeField] private Vector3 _dropRadius;
    [SerializeField] private float _dropDuration;

    public Vector3 DropRadius => _dropRadius;
    public float dropDuration => _dropDuration;
}
