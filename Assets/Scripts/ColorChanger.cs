using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defaultColor = Color.white;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        SetDefaultColor();
    }

    public void ChangeColor()
    {
        _renderer.material.color = Random.ColorHSV(); 
    }

    public void SetDefaultColor()
    {
        _renderer.material.color = _defaultColor;
    }
}