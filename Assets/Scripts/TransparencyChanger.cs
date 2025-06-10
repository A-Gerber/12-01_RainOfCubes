using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TransparencyChanger : MonoBehaviour
{
    private Renderer _renderer;
    private float _defaultValue = 1;

    private Material _material;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        _material = _renderer.material;
    }

    public void ChangeTransparency(float value)
    {
        Color color = _material.color;
        color.a = value;
        _material.color = color;
    }

    public void SetDefault()
    {
        Color color = _material.color;
        color.a = _defaultValue;
        _material.color = color;
    }
}