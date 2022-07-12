using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBorderData", menuName = "GDData/Player/BorderData", order = 1)]
public class BorderData : ScriptableObject
{
    [SerializeField] private Vector3 _minPos;
    [SerializeField] private Vector3 _maxPos;

    public Vector3 MinPosition => _minPos;
    public Vector3 MaxPosition => _maxPos;
}
