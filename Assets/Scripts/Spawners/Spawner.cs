using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    protected ObjectPool<T> _pool;
    protected SpawnerData _spawnerData;

    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    public event Action<SpawnerData> ChangedSpawnerData;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Create(),
            actionOnGet: (@object) => OnGet(@object),
            actionOnRelease: (@object) => OnRelease(@object),
            actionOnDestroy: (@object) => Destroy(@object.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        _spawnerData = new SpawnerData(0, 0, 0);
        ChangedSpawnerData?.Invoke(_spawnerData);
    }

    protected virtual T Create()
    {
        _spawnerData.IncreaseCreatedObjects();        
        ChangedSpawnerData?.Invoke(_spawnerData);

        return Instantiate(_prefab);
    }

    protected virtual void OnRelease(T @object)
    {
        @object.gameObject.SetActive(false);

        _spawnerData.ReduceActiveObjects();
        ChangedSpawnerData?.Invoke(_spawnerData);
    }

    protected virtual void OnGet(T @object)
    {
        @object.gameObject.SetActive(true);

        _spawnerData.IncreaseSpawnedObjects();
        _spawnerData.IncreaseActiveObjects();
        ChangedSpawnerData?.Invoke(_spawnerData);
    }

    protected virtual void Get()
    {
        _pool.Get();
    }

    protected virtual void Release(T @object)
    {
        _pool.Release(@object);
    }
}
