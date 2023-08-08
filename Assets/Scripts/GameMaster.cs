using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private PlayerController[] _players;
    [SerializeField] private Vector3 _cardPreviewPosition;
    [SerializeField] private Quaternion _cardPreviewRotation;
    [SerializeField] private CardStack[] _cardStacks;
    [SerializeField] private GameEvent _dice;
    [SerializeField] private BooleanReference[] _diceStables;
    [SerializeField] private IntegerReference[] _diceValues;
    private PlayerController CurrentPlayer => _players[_currentPlayerId];
    private int _currentPlayerId;
    private int _numberOfStableDice;
    private int _currentDiceValue;
    private bool _hasDiced;
    private Card _currentCard;
    private CardStack _currentCardStack;
    private Vector3 _currentCardOriginalPosition;
    private Quaternion _currentCardOriginalRotation;

    private UnityAction _update;

    private void Awake()
    {
        _currentPlayerId = 0;
    }

    private void Update()
    {
        _update?.Invoke();

        if (!_hasDiced)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _hasDiced = true;
                _dice.Raise();
                _update = CheckDice;
            }
        }
    }

    private void CheckDice()
    {
        foreach (var diceStable in _diceStables)
            if (diceStable.Value == false)
                return;
        
        ReadDices();
        _update = null;
    }

    private void ReadDices()
    {
        var sumDiceValues = 0;
        foreach (var diceValue in _diceValues)
            sumDiceValues += diceValue;
        
        CurrentPlayer.Move(sumDiceValues);
    }

    public void DrawCard(Identity identity)
    {
        foreach (var stack in _cardStacks)
        {
            if (stack.Identity == identity)
            {
                _currentCardStack = stack;
                _currentCard = _currentCardStack.Draw();
                StartCoroutine(PreviewCard());
                return;
            }
        }
    }

    public void TakeCard()
    {
        // CurrentPlayer.AddCard(_currentCard);
        _currentCard = null;
    }

    public void ReturnCard()
    {
        _currentCardStack.Return(_currentCard);
        StartCoroutine(PutCard2Stack());
    }

    public void EndTurn()
    {
        // CurrentPlayer.EndTurn();
        _currentPlayerId++;
        if (_currentPlayerId >= _players.Length)
            _currentPlayerId = 0;
        // CurrentPlayer.OnTurn();
        _hasDiced = false;
    }

    private IEnumerator PreviewCard()
    {
        var timer = 0f;
        var duration = 0.5f;
        var cardTransform = _currentCard.transform;
        _currentCardOriginalPosition = cardTransform.position;
        _currentCardOriginalRotation = cardTransform.rotation;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
            var t = timer / duration;
            cardTransform.position = Vector3.Lerp(_currentCardOriginalPosition, _cardPreviewPosition, t);
            cardTransform.rotation = Quaternion.Slerp(_currentCardOriginalRotation, _cardPreviewRotation, t);
        }
    }

    private IEnumerator PutCard2Stack()
    {
        var timer = 0f;
        var duration = 0.5f;
        var cardTransform = _currentCard.transform;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
            var t = timer / duration;
            cardTransform.position = Vector3.Lerp(_cardPreviewPosition, _currentCardOriginalPosition, t);
            cardTransform.rotation = Quaternion.Slerp(_cardPreviewRotation, _currentCardOriginalRotation, t);
        }

        _currentCard = null;
    }
}