using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Board Layout", fileName = "BoardLayout")]
public class BoardLayout : ScriptableObject
{
    public List<BlockAttribute> Blocks;
}

[Serializable]
public struct BlockAttribute
{
    public string Name;
    [FormerlySerializedAs("Visual")] public Color BlockColor;
    public Color TextColor;
    public UnityEvent Activation;
}
