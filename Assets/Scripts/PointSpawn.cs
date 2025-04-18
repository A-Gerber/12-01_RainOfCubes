using System.Collections;
using UnityEngine;

public class PointSpawn : MonoBehaviour
{
    private int _minHeight = 15;
    private int _maxHeight = 18;
    private int _minHorizontalPosition = 1;
    private int _maxHorizontalPosition = 18;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;
    private float _repeatRate = 1f;

    private void Awake()
    {
        _wait = new WaitForSeconds(_repeatRate);
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(ChangePositionOverTime());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void ChangePosition()
    {     
        transform.position =  new Vector3(
            UnityEngine.Random.Range(_minHorizontalPosition, _maxHorizontalPosition + 1),
            UnityEngine.Random.Range(_minHeight, _maxHeight + 1),
            UnityEngine.Random.Range(_minHorizontalPosition, _maxHorizontalPosition + 1));
    }

    private IEnumerator ChangePositionOverTime()
    {
        while (gameObject.activeSelf)
        {
            yield return _wait;
            ChangePosition();
        }
    }
}