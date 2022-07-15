using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Transform _stack;
    [SerializeField] private UnloadLogic _unloadLogic;
    [Space]
    [SerializeField] private float _unloadDelay;
    [Space]
    [SerializeField] private GameData _playerData;
    [SerializeField] private StackUI _stackUI;

    private bool _unloading;

    private Stack<PlantBlockLogic> _plantBlocks = new Stack<PlantBlockLogic>();

    private void Start()
    {
        _stackUI.ChangeProgressBar(_plantBlocks.Count, _playerData.MaxStack);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_unloading && other.TryGetComponent(out PlantBlockLogic plantBlock) && _plantBlocks.Count < _playerData.MaxStack) 
        {
            plantBlock.CollectAnimation(_stack, _plantBlocks.Count);
            _plantBlocks.Push(plantBlock);
            _stackUI.ChangeProgressBar(_plantBlocks.Count, _playerData.MaxStack);
        }
        else if(other.TryGetComponent(out UnloadAnimationTrigger unloadPlace))
        {
            _unloading = true;
            StartCoroutine(Unload(unloadPlace.CollectorPoint));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<UnloadAnimationTrigger>())
        {
            _unloadLogic.ResetAnimation();
            _unloading = false;
        }
    }

    private IEnumerator Unload(Transform collectorPoint) 
    {
        while (_unloading && _plantBlocks.Count > 0)
        {
            PlantBlockLogic plantBlock = _plantBlocks.Pop();
            _unloadLogic.AddBlock(plantBlock);
            plantBlock.CollectAnimation(collectorPoint, 0);
            _stackUI.ChangeProgressBar(_plantBlocks.Count, _playerData.MaxStack);
            yield return new WaitForSeconds(_unloadDelay);
        }
    }
}
