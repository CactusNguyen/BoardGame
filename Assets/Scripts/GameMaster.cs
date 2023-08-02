using System;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Ins;
    
    [SerializeField] private Player[] _players;
    [SerializeField] private Dice[] _dices;
    private int _currentPlayerId;
    private int _numberOfStableDice;
    private int _currentDiceValue;

    private void Awake()
    {
        Ins = this;
        _currentPlayerId = 0;
    }

    private void Start()
    {
        foreach (var dice in _dices)
            dice.OnStabilize += ReadDiceValue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dice();
        }
    }

    private void Dice()
    {
        _currentDiceValue = 0;
        _numberOfStableDice = 0;
        foreach (var dice in _dices)
        {
            dice.DoDice();
        }
    }

    private void ReadDiceValue(int value)
    {
        _currentDiceValue += value;
        _numberOfStableDice++;
        if (_numberOfStableDice >= _dices.Length)
        {
            _players[_currentPlayerId].Move(_currentDiceValue);
            _currentPlayerId++;
            if (_currentPlayerId >= _players.Length)
                _currentPlayerId = 0;
        }
    }
}
