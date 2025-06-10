using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour, IPushable
{
    private Rigidbody _rigidbody;

    public event Action<Cube> ReleasedCube;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            ReleasedCube?.Invoke(this);
        }
    }

    public Rigidbody GetRigidbody() => _rigidbody;
}