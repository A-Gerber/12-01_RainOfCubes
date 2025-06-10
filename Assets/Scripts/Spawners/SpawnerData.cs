public class SpawnerData
{
    public SpawnerData(int spawnedObjects, int createdObjects, int activeObjects)
    {
        SpawnedObjects = spawnedObjects;
        CreatedObjects = createdObjects;
        ActiveObjects = activeObjects;
    }

    public int SpawnedObjects { get; private set; }
    public int CreatedObjects { get; private set; }
    public int ActiveObjects { get; private set; }

    public void IncreaseSpawnedObjects() => SpawnedObjects++;
    public void IncreaseCreatedObjects() => CreatedObjects++;
    public void IncreaseActiveObjects() => ActiveObjects++;
    public void ReduceActiveObjects() => ActiveObjects--;
}