using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collector : MonoBehaviour
{
    [SerializeField] private Transform _collectorPoint;
    [SerializeField] private int _maxStack;

    private Stack<PlantBlockLogic> _plantBlocks = new Stack<PlantBlockLogic>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlantBlockLogic plantBlock) && _plantBlocks.Count < _maxStack) 
        {
            plantBlock.CollectAnimation(_collectorPoint, _plantBlocks.Count);
            _plantBlocks.Push(plantBlock);


        }
        //else if(other.TryGetComponent(out PlantBlockLogic plantBlock))
        {

        }
    }
}
