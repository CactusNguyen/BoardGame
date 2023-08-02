using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Piece : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Vector3 _randomPositionArea;
    private static MaterialPropertyBlock _mpb;
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private Vector3 _position;

    private void Start()
    {
        _position = transform.position;
    }

    public void Init(PieceAttribute attribute)
    {
        _text.text = attribute.Content;
        _mpb ??= new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_mpb);
        _mpb.SetColor(BaseColor, attribute.GroundColor);
        _renderer.SetPropertyBlock(_mpb);
    }

    public Vector3 RandomPositionOnPiece()
    {
        return _position + new Vector3(Random.Range(-_randomPositionArea.x / 2, _randomPositionArea.x / 2), 0,
            Random.Range(-_randomPositionArea.z / 2, _randomPositionArea.z / 2));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, _randomPositionArea);
    }
}