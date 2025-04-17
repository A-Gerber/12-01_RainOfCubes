using UnityEngine;
using UnityEngine.Pool;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabCube;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefabCube),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void OnTriggerEnter(Collider other)
    {
        _pool.Release(other.gameObject);
    }

    public void ActionOnRelease(GameObject obj)
    {
        obj.SetActive(false);

        obj.TryGetComponent<Cube>(out Cube cube);
        cube.Released -= ReleaseCube;
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.transform.position = _startPoint.transform.position;
        obj.SetActive(true);

        obj.TryGetComponent<Cube>(out Cube cube);
        cube.Released += ReleaseCube;
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void ReleaseCube(GameObject obj)
    {
        _pool.Release(obj);
    }
}