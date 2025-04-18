using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefabCube;
    [SerializeField] private PointSpawn _startPoint;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Cube> _pool;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefabCube),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
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

    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);

        cube.Released -= ReleaseCube;
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = _startPoint.transform.position;
        cube.gameObject.SetActive(true);

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