using System.Collections;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Ins;

    [SerializeField] private Player[] _players;
    [SerializeField] private Dice[] _dices;
    [SerializeField] private Vector3 _cardPreviewPosition;
    [SerializeField] private Quaternion _cardPreviewRotation;
    [SerializeField] private CardStack[] _cardStacks;
    private int _currentPlayerId;
    private int _numberOfStableDice;
    private int _currentDiceValue;
    public Player CurrentPlayer => _players[_currentPlayerId];
    private bool _hasDiced;
    private Card _currentCard;
    private CardType _currentCardType;
    private Vector3 _currentCardOriginalPosition;
    private Quaternion _currentCardOriginalRotation;

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
        if (!_hasDiced)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _hasDiced = true;
                Dice();
            }
            return;
        }
        
        HUD.Ins.SetActionMenuVisibility(CurrentPlayer.Ready2End && _currentCard == null);
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
        }
    }

    public void DrawCard(CardType cardType)
    {
        _currentCardType = cardType;
        _currentCard = _cardStacks[(int)_currentCardType].Draw();
        StartCoroutine(PreviewCard());
    }

    public void TakeCard()
    {
        CurrentPlayer.AddCard(_currentCard);
        _currentCard = null;
    }

    public void ReturnCard()
    {
        _cardStacks[(int)_currentCardType].Return(_currentCard);
        StartCoroutine(PutCard2Stack());
    }

    public void EndTurn()
    {
        CurrentPlayer.EndTurn();
        _currentPlayerId++;
        if (_currentPlayerId >= _players.Length)
            _currentPlayerId = 0;
        CurrentPlayer.OnTurn();
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