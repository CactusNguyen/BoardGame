using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    private CardAttribute _attribute;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TMP_Text _text;

    public CardAttribute Attribute
    {
        get => _attribute;
        set => _attribute = value;
    }

    private void Start()
    {
        _renderer.material.SetColor("_BaseColor", _attribute.Color);
        _text.text = _attribute.Name + ":" + _attribute.Detail;
        _text.color = _attribute.TextColor;
    }
}