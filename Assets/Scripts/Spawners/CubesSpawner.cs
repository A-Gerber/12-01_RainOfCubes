using System;
using System.Collections;
using UnityEngine;

public class CubesSpawner : Spawner <Cube>
{
    [SerializeField] private float _repeatRate = 1f;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private int _minHeight = 15;
    private int _maxHeight = 18;
    private int _minHorizontalPosition = 1;
    private int _maxHorizontalPosition = 18;

    public event Action<Cube> Released;

    protected override void Awake()
    {
        base.Awake();

        _wait = new WaitForSeconds(_repeatRate);
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(GetCubesOverTime());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    protected override void OnRelease(Cube cube)
    {
        base.OnRelease(cube);

        cube.ReleasedCube -= Release;
    }

    protected override void OnGet(Cube cube)
    {
        base.OnGet(cube);

        cube.transform.position = new Vector3(
            UnityEngine.Random.Range(_minHorizontalPosition, _maxHorizontalPosition + 1),
            UnityEngine.Random.Range(_minHeight, _maxHeight + 1),
            UnityEngine.Random.Range(_minHorizontalPosition, _maxHorizontalPosition + 1));

        cube.ReleasedCube += Release;
    }

    protected override void Release(Cube cube)
    {
        base.Release(cube);

        Released?.Invoke(cube);
    }

    private IEnumerator GetCubesOverTime()
    {
        while (gameObject.activeSelf)
        {
            yield return _wait;
            Get();
        }
    }
}