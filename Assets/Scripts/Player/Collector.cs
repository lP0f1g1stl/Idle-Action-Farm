using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Transform _stack;
    [SerializeField] private UnloadLogic _unloadLogic;
    [Space]
    [SerializeField] private GameData _gameData;
    [SerializeField] private StackUI _stackUI;

    private bool _unloading;

    private Stack<PlantBlockLogic> _plantBlocks = new Stack<PlantBlockLogic>();

    private void Start()
    {
        _stackUI.ChangeProgressBar(_plantBlocks.Count, _gameData.MaxStack);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_unloading && other.TryGetComponent(out PlantBlockLogic plantBlock) && _plantBlocks.Count < _gameData.MaxStack) 
        {
            Load(plantBlock);
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
    private void Load(PlantBlockLogic plantBlock)
    {

        plantBlock.CollectAnimation(_stack, CalculatePosInStack());
        plantBlock.ChangeScale();
        _plantBlocks.Push(plantBlock);
        _stackUI.ChangeProgressBar(_plantBlocks.Count, _gameData.MaxStack);
    }
    private Vector3 CalculatePosInStack() 
    {
        float xPos = (-_gameData.LineLength + 1)/2 + (_plantBlocks.Count % _gameData.LineLength);
        return new Vector3(xPos, Mathf.CeilToInt(_plantBlocks.Count / _gameData.LineLength));
    }

    private IEnumerator Unload(Transform collectorPoint) 
    {
        while (_unloading && _plantBlocks.Count > 0)
        {
            PlantBlockLogic plantBlock = _plantBlocks.Pop();
            _unloadLogic.AddBlock(plantBlock);
            plantBlock.CollectAnimation(collectorPoint, Vector3.zero);
            plantBlock.RevertScale();
            _stackUI.ChangeProgressBar(_plantBlocks.Count, _gameData.MaxStack);
            yield return new WaitForSeconds(_gameData.UnloadDelay);
        }
    }
}
