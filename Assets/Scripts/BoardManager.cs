using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Ins;

    [SerializeField] private BoardLayout _layout;
    [SerializeField] private Piece _piecePrefab;
    [SerializeField] private Transform[] _pieceTransforms;
    private Piece[] _pieces;

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        LoadLayout();
    }

    private void LoadLayout()
    {
        InitiatePieces();
        LoadPiecesContents();
    }

    private void InitiatePieces()
    {
        _pieces = new Piece[_pieceTransforms.Length];
        for (var i = 0; i < _pieceTransforms.Length; i++)
        {
            _pieces[i] = Instantiate(_piecePrefab, _pieceTransforms[i]);
        }
    }

    private void LoadPiecesContents()
    {
        for (var i = 0; i < _pieces.Length; i++)
        {
            _pieces[i].Init(_layout.Pieces[i]);
        }
    }

    public Vector3 GetPiecePosition(int pieceId)
    {
        return _pieces[pieceId].RandomPositionOnPiece();
    }
}