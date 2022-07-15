using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Transform _collectorPoint;

    public Transform CollectorPoint => _collectorPoint;
}
