using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameData", menuName = "GDData/Player/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField] private int _money;
    [Space]
    [SerializeField] private int _maxStack;
    [SerializeField] private int _lineLength;
    [SerializeField] private float _unloadDelay;
    [Space]
    [SerializeField] private int _blockPrice;

    public int Money { get => _money; set => _money = value; }
    public int MaxStack => _maxStack;
    public int LineLength => _lineLength;
    public float UnloadDelay => _unloadDelay;
    public int BlockPrice => _blockPrice;
}
