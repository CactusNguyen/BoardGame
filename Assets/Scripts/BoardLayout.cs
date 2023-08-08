using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Board Layout", fileName = "BoardLayout")]
public class BoardLayout : ScriptableObject
{
    public List<BlockAttribute> Blocks;

    [ContextMenu("Sync")]
    public void Sync()
    {
        var cached = new Dictionary<string, BlockAttribute>();
        for (var i = 0; i < Blocks.Count; i++)
        {
            var block = Blocks[i];
            if (!cached.ContainsKey(block.Name))
            {
                cached.Add(block.Name, block);
            }
            else
            {
                block.BlockColor = cached[block.Name].BlockColor;
                block.TextColor = cached[block.Name].TextColor;
                block.Activation = cached[block.Name].Activation;
            }

            Blocks[i] = block;
        }
    }
}

[Serializable]
public struct BlockAttribute
{
    public string Name;
    [FormerlySerializedAs("Visual")] public Color BlockColor;
    public Color TextColor;
    public UnityEvent Activation;
}