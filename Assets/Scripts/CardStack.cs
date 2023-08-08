using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    [SerializeField] private Identity _identity;
    public Identity Identity => _identity;
    [SerializeField] private CardAttribute[] _cardTypes;
    [SerializeField] private int[] _numberPerType;
    [SerializeField] private Card _cardPrefab;
    private List<Card> _stack;

    private const float CardThickness = 0.02f;

    private void Start()
    {
        _stack = new List<Card>();
        for (var i = 0; i < _cardTypes.Length && i < _numberPerType.Length; i++)
        {
            for (var j = 0; j < _numberPerType[i]; j++)
            {
                var card = Instantiate(_cardPrefab, transform);
                card.Attribute = _cardTypes[i];
                _stack.Add(card);
                var cardLocalPosition = new Vector3(0, CardThickness * _stack.Count, 0);
                card.transform.localPosition = cardLocalPosition;
            }
        }

        _stack.Shuffle();
    }

    public Card Draw()
    {
        var card = _stack.Last();
        _stack.RemoveAt(_stack.Count - 1);
        return card;
    }

    public void Return(Card card)
    {
        _stack.Add(card);
    }
}