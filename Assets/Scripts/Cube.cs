using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ColorChanger))]
public class Cube : MonoBehaviour, IInstantiating
{
    private ColorChanger _colorChanger;
    private WaitForSeconds _wait;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;

    public event Action<GameObject> Released;

    public Rigidbody Rigidbody { get; private set; }
    public GameObject CollisionCube { get; private set; }
    public bool HaveCollision { get; private set; } = false;



    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        Rigidbody = GetComponent<Rigidbody>();

        Rigidbody.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform) && HaveCollision == false)
        {
            _colorChanger.ChangeColor();           
            HaveCollision = true;
            _wait = new WaitForSeconds(UnityEngine.Random.Range(_minLifetime, _maxLifetime + 1));

            StartCoroutine(DieOverTime());
        }
    }

    private IEnumerator DieOverTime()
    {
        yield return _wait;
        HaveCollision = false;
        _colorChanger.SetStartColor();

        GameObject thisObject = this.gameObject;

        Released?.Invoke(thisObject);
    }
}