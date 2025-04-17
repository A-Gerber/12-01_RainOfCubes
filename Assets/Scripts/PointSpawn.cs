
using UnityEngine;

public class PointSpawn : MonoBehaviour
{
    private int _minHeight = 15;
    private int _maxHeight = 18;
    private int _minHorizontalPosition = 1;
    private int _maxHorizontalPosition = 18;

    private float _delay = 0f;
    private float _repeatRate = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(ChangePosition), _delay, _repeatRate);
    }

    private void ChangePosition()
    {     
        transform.position =  new Vector3(
            UnityEngine.Random.Range(_minHorizontalPosition, _maxHorizontalPosition + 1),
            UnityEngine.Random.Range(_minHeight, _maxHeight + 1),
            UnityEngine.Random.Range(_minHorizontalPosition, _maxHorizontalPosition + 1));
    }
}