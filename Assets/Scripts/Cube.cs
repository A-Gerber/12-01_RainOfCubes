using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    private ColorChanger _colorChanger;
    private WaitForSeconds _wait;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;
    private bool _isCollided = false;
    private Rigidbody _rigidbody;

    public event Action<Cube> Released;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform) && _isCollided == false)
        {
            _colorChanger.ChangeColor();           
            _isCollided = true;
            _wait = new WaitForSeconds(UnityEngine.Random.Range(_minLifetime, _maxLifetime + 1));

            StartCoroutine(DieOverTime());
        }
    }

    private IEnumerator DieOverTime()
    {
        yield return _wait;
        _isCollided = false;
        _colorChanger.SetStartColor();

        Released?.Invoke(this);
    }
}