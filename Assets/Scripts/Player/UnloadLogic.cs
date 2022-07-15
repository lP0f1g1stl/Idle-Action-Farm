using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnloadLogic : MonoBehaviour
{
    [SerializeField] private MoneyUI _moneyUI;
    [SerializeField] private GameData _gameData;


    private Queue<PlantBlockLogic> _plantBlocks = new Queue<PlantBlockLogic>();

    private void Start()
    {
        _moneyUI.SetGameData(_gameData);
    }
    public void AddBlock(PlantBlockLogic plantBlock) 
    {
        _plantBlocks.Enqueue(plantBlock);
        plantBlock.OnAnimationComplete += RemoveBlock;
    }

    private void RemoveBlock() 
    {
        PlantBlockLogic plantBlock = _plantBlocks.Dequeue();
        plantBlock.OnAnimationComplete -= RemoveBlock;
        Destroy(plantBlock.gameObject);
        _moneyUI.ChangeMoney();
    }

    public void ResetAnimation() 
    {
        _moneyUI.ResetCoins();
    }
}
