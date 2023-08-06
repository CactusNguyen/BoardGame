using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Vector3 _randomPositionArea;
    private static MaterialPropertyBlock _mpb;
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private Vector3 _position;
    private BlockAttribute _attribute;

    private void Start()
    {
        _position = transform.position;
    }

    public void Init(BlockAttribute attribute)
    {
        _attribute = attribute;
        _text.text = _attribute.Name;
        _text.color = _attribute.TextColor;
        _mpb ??= new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_mpb);
        _mpb.SetColor(BaseColor, _attribute.BlockColor);
        _renderer.SetPropertyBlock(_mpb);
    }

    public Vector3 RandomPositionOnBlock()
    {
        return _position + new Vector3(Random.Range(-_randomPositionArea.x / 2, _randomPositionArea.x / 2), 0,
            Random.Range(-_randomPositionArea.z / 2, _randomPositionArea.z / 2));
    }

    public void Active()
    {
        _attribute.Activation.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, _randomPositionArea);
    }
}