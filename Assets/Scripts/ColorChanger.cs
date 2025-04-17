using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private Color _startColor = Color.white;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        SetStartColor();
    }

    public void ChangeColor()
    {
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    public void SetStartColor()
    {
        _renderer.material.color = _startColor;
    }
}