using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Card", fileName = "Card")]
public class CardAttribute : ScriptableObject
{
    public string Name;
    public Identity Identity;
    public string Detail;
    public Color Color;
    public Color TextColor;
    public UnityEvent OnFinishTurn;
    public UnityEvent OnFinishCycle;
}
