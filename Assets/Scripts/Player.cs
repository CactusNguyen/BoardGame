using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform _transform;
    private int _pieceId;

    private void Awake()
    {
        _transform = transform;
    }

    private IEnumerator Start()
    {
        yield return null;
        yield return null;
        _transform.position = BoardManager.Ins.GetPiecePosition(_pieceId);
    }

    public void Move(int movement)
    {
        StartCoroutine(MoveCoroutine(movement));
    }

    private IEnumerator MoveCoroutine(int movement)
    {
        for (var i = 0; i < movement; i++)
        {
            yield return SingleMove();
            _pieceId++;
        }
    }

    private IEnumerator SingleMove()
    {
        var timer = 0f;
        var duration = 0.12f;
        var previousPosition = _transform.position;
        var targetPosition = BoardManager.Ins.GetPiecePosition(_pieceId + 1);
        var position = previousPosition;
        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            var t = timer / duration;
            position = Vector3.Lerp(previousPosition, targetPosition, t);

            position.y = Mathf.Pow(4f * t * (1f - t), 2);
            _transform.position = position;
        }
    }
}