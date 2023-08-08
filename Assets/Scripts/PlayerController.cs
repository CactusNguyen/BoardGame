using UnityEngine;

[CreateAssetMenu]
public class PlayerController : ScriptableObject
{
    [SerializeField] private Player _player;

    public void Assign(Player player)
    {
        _player = player;
    }

    public void Move(int movement)
    {
        _player.Move(movement);
    }

    public void ChangeFinance(int amount)
    {
        _player.ChangeFinance(amount);
    }
}
