using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Board Layout", fileName = "BoardLayout")]
public class BoardLayout : ScriptableObject
{
    public List<PieceAttribute> Pieces;
}
