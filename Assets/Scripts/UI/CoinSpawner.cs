using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private CoinAnimation _coinPrefab;
    [SerializeField] private CoinAnimation[] _coins;
    [Space]
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;
    [SerializeField] private float _coinsFlightDuration;


    private GameData _gameData;
    private int _currentCoin;

    public event Action OnCoinEarned;

    public void Init(GameData gameData) 
    {
        _gameData = gameData;
        CreateUICoins();
        AddListner();
    }

    public void CreateUICoins()
    {
        int coinsLength = Mathf.CeilToInt(_coinsFlightDuration / _gameData.UnloadDelay);
        _coins = new CoinAnimation[coinsLength];
        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i] = Instantiate(_coinPrefab, transform);
        }
    }
    private void AddListner()
    {
        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i].OnAnimationComplete += OnCoinEarned;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i].OnAnimationComplete -= OnCoinEarned;
        }
    }
    public void ShowCoin()
    {
        Vector3 startPos = Camera.main.WorldToScreenPoint(_startPos.position);
        _coins[_currentCoin].AnimateCoin(startPos, _endPos, _coinsFlightDuration);
        _currentCoin++;
        if (_currentCoin >= _coins.Length)
        {
            _currentCoin = 0;
        }
    }

}
