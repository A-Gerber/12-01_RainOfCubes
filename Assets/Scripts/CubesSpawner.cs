using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefabCube;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Cube> _pool;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private int _minHeight = 15;
    private int _maxHeight = 18;
    private int _minHorizontalPosition = 1;
    private int _maxHorizontalPosition = 18;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefabCube),
            actionOnGet: (cube) => ActWhenGet(cube),
            actionOnRelease: (cube) => ActWhenRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

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

    private void ActWhenRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);

        cube.Released -= ReleaseCube;
    }

    private void ActWhenGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.transform.position = new Vector3(
            UnityEngine.Random.Range(_minHorizontalPosition, _maxHorizontalPosition + 1),
            UnityEngine.Random.Range(_minHeight, _maxHeight + 1),
            UnityEngine.Random.Range(_minHorizontalPosition, _maxHorizontalPosition + 1));

        cube.Released += ReleaseCube;
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
    }

    private IEnumerator GetCubesOverTime()
    {
        while (gameObject.activeSelf)
        {
            yield return _wait;
            GetCube();
        }
    }
}