using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [Space]
    [SerializeField] private CoinAnimation[] _coins;
    [Space]
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;
    [SerializeField] private float _coinsFlightDuration;

    private Vector3 _defaultPos;
    private int _currentCoin;
    private GameData _gameData;
    private Tween _shaking;
    public void SetGameData( GameData gameData) 
    {
        _defaultPos = transform.position;
        _gameData = gameData;
        ChangeMoneyText();
    }
    private void OnEnable()
    {
        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i].OnAnimationComplete += AddMoney;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i].OnAnimationComplete -= AddMoney;
        }
    }
    public void ChangeMoney() 
    {
        Vector3 startPos = Camera.main.WorldToScreenPoint(_startPos.position);
        _coins[_currentCoin].AnimateCoin(startPos, _endPos, _coinsFlightDuration);
        _currentCoin++;
    }

    private void AddMoney() 
    {
        _shaking = transform.DOShakePosition(0.5f, 5);
        _gameData.Money += _gameData.BlockPrice;
        ChangeMoneyText();
    }
    private void ChangeMoneyText() 
    {
        _text.text = _gameData.Money.ToString();
    }

    public void ResetCoins() 
    {
        _currentCoin = 0;
        transform.position = _defaultPos;
    }
}
