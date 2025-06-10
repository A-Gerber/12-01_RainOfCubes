using TMPro;
using UnityEngine;

public class SpawnersView<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Spawner<T> _cubesSpawner;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _cubesSpawner.ChangedSpawnerData += UpdateData;
    }

    private void OnDisable()
    {
        _cubesSpawner.ChangedSpawnerData -= UpdateData;
    }

    private void UpdateData(SpawnerData spawnerData)
    {
        _text.text = $"Заспавненых \t- {spawnerData.SpawnedObjects}" +
                     $"\nСозданных \t\t- {spawnerData.CreatedObjects}" +
                     $"\nАктивных \t\t- {spawnerData.ActiveObjects}";
    }
}
