using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    [SerializeField] private Vector3[] _faceNormals;
    private Rigidbody _rb;
    private Transform _transform;

    private bool _isStable = true;
    public event Action<int> OnStabilize;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
    }

    public void DoDice()
    {
        _transform.position += Vector3.up * 5;
        _rb.angularVelocity = Random.insideUnitSphere;
        _rb.velocity = Vector3.down * 0.5f;
        _isStable = false;
    }

    private void Update()
    {
        if (_isStable || _rb.angularVelocity.sqrMagnitude > 0.01f) return;
        
        _isStable = true;
        for (var i = 0; i < _faceNormals.Length; i++)
        {
            var dotValue = Vector3.Dot(Vector3.up, _transform.TransformDirection(_faceNormals[i]));
            if (dotValue > 0.8f)
                OnStabilize?.Invoke(i + 1);
        }
    }
}