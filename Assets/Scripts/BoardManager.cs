using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Ins;

    [SerializeField] private BoardLayout _layout;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private float _blockSize;
    private Vector3[] _blockPositions;
    public int BlockCount { get; private set; }
    private Block[] _blocks;

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        LoadLayout();
    }
    
    private void OnValidate()
    {
        InitiateBlockPositions();
    }

    private void LoadLayout()
    {
        InitiateBlockPositions();
        InitiateBlocks();
        LoadBlockAttributes();
    }

    private void InitiateBlockPositions()
    {
        BlockCount = 10 * 4;
        _blockPositions = new Vector3[BlockCount];
        var i = 0;
        for (; i < 10; i++)
        {
            _blockPositions[i] = new Vector3((5 - i) * _blockSize, 0, -5 * _blockSize);
        }

        for (; i < 20; i++)
        {
            _blockPositions[i] = new Vector3(-5 * _blockSize, 0, (i - 10 - 5) * _blockSize);
        }

        for (; i < 30; i++)
        {
            _blockPositions[i] = new Vector3((i - 20 - 5) * _blockSize, 0, 5 * _blockSize);
        }

        for (; i < 40; i++)
        {
            _blockPositions[i] = new Vector3(5 * _blockSize, 0, (5 - i + 30) * _blockSize);
        }
    }

    private void InitiateBlocks()
    {
        _blocks = new Block[_blockPositions.Length];
        for (var i = 0; i < _blockPositions.Length; i++)
        {
            _blocks[i] = Instantiate(_blockPrefab, _blockPositions[i], Quaternion.identity);
        }
    }

    private void LoadBlockAttributes()
    {
        for (var i = 0; i < _blocks.Length; i++)
        {
            _blocks[i].Init(_layout.Blocks[i]);
        }
    }

    public Vector3 GetPositionOnBlock(int blockId)
    {
        return _blocks[blockId].RandomPositionOnBlock();
    }

    public Block GetBlock(int blockId)
    {
        return _blocks[blockId];
    }

    private void OnDrawGizmos()
    {
        if (_blockPositions == null || _blockPositions.Length == 0)
            InitiateBlockPositions();

        if (_blockPositions == null) return;

        foreach (var blockPosition in _blockPositions)
            Gizmos.DrawCube(blockPosition, Vector3.one * _blockSize * 0.95f);
    }
}