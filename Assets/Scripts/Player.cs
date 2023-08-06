using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _money = 2000;
    public bool Ready2End;
    private Transform _transform;
    private int _blockId;
    private List<Card> _cards;

    private void Awake()
    {
        _transform = transform;
    }

    public void OnTurn()
    {
        Ready2End = false;
    }

    private IEnumerator Start()
    {
        _cards = new List<Card>();
        _renderer.material.SetColor("_BaseColor", _color);
        yield return null;
        yield return null;
        _transform.position = BoardManager.Ins.GetPositionOnBlock(_blockId);
    }

    public void Move(int movement)
    {
        StartCoroutine(MoveCoroutine(movement));
    }
    
    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public void ChangeFinance(int changeAmount)
    {
        _money += changeAmount;
        if (_money <= 0)
            Debug.Log("Bankrupt");
    }

    private void FinishCycle()
    {
        foreach (var card in _cards)
            card.Attribute.OnFinishCycle.Invoke();
    }

    public void EndTurn()
    {
        foreach (var card in _cards)
        {
            card.Attribute.OnFinishTurn.Invoke();
        }
    }
    
    
    private IEnumerator MoveCoroutine(int movement)
    {
        Ready2End = false;
        for (var i = 0; i < movement; i++)
        {
            _blockId++;
            if (_blockId > BoardManager.Ins.BlockCount - 1)
            {
                FinishCycle();
                _blockId = 0;
            }
            yield return SingleMove();
        }

        Ready2End = true;
        BoardManager.Ins.GetBlock(_blockId).Active();
    }

    private IEnumerator SingleMove()
    {
        var timer = 0f;
        var duration = 0.12f;
        var previousPosition = _transform.position;
        var targetPosition = BoardManager.Ins.GetBlock(_blockId).RandomPositionOnBlock();
        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            var t = timer / duration;
            var position = Vector3.Lerp(previousPosition, targetPosition, t);

            position.y = Mathf.Pow(4f * t * (1f - t), 2);
            _transform.position = position;
        }
    }

}