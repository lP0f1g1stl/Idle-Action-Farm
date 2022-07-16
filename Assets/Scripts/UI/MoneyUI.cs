using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [Space]
    [SerializeField] private CoinSpawner _coinSpawner;

    private Vector3 _defaultPos;
    private GameData _gameData;

    public CoinSpawner CoinSpawner => _coinSpawner;
    public void Init( GameData gameData) 
    {
        _defaultPos = transform.position;
        _gameData = gameData;
        ChangeMoneyText();
        _coinSpawner.Init(gameData);
    }
    private void OnEnable()
    {
        _coinSpawner.OnCoinEarned += AddMoney;
    }
    private void OnDisable()
    {
        _coinSpawner.OnCoinEarned -= AddMoney;
    }

    private void AddMoney() 
    {
        transform.DOShakePosition(0.5f, 5);
        _gameData.Money += _gameData.BlockPrice;
        ChangeMoneyText();
    }
    private void ChangeMoneyText() 
    {
        _text.text = _gameData.Money.ToString();
    }

    public void ResetUI() 
    {
        transform.position = _defaultPos;
    }
}
