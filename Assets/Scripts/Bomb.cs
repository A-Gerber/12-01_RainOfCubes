using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(TransparencyChanger))]
public class Bomb : MonoBehaviour, IPushable
{
    private const float ErrorRate = 0.005f;

    private Rigidbody _rigidbody;
    private WaitForSeconds _wait;

    private float _delay = 0.2f;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;

    public event Action<Bomb> ReleasedBomb;

    public TransparencyChanger TransparencyChanger { get; private set; }

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;

        TransparencyChanger = GetComponent<TransparencyChanger>();
    }

    private void OnEnable()
    {
        StartCoroutine(DieOverTime());
    }

    public Rigidbody GetRigidbody() => _rigidbody;

    private IEnumerator DieOverTime()
    {
        float lifeTime = CalculateLifeTime();
        float time = 0;

        while (lifeTime - time > ErrorRate)
        {
            yield return _wait;
            time += _delay;

            TransparencyChanger.ChangeTransparency(1 - time / lifeTime);
        }

        ReleasedBomb?.Invoke(this);
    }

    private float CalculateLifeTime() => UnityEngine.Random.Range(_minLifetime, _maxLifetime + 1);
}