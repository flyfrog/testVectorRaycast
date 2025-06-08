using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [Header("Materials")] 
    [SerializeField] private Material _defaultMaterial; // Красный
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _hitColor;
    [SerializeField] private TextMeshPro[]  _texts;
    private Material _cachedMaterial;

    public void Construct(int index)
    {
        DrawIndexText(index.ToString());
    }

    public void SetHitColor()
    {
        _cachedMaterial.color = _hitColor;
    }

    public void SetDefaultColor()
    {
        _cachedMaterial.color = _defaultColor;
    }

    private void Start()
    {
        _cachedMaterial = new Material(_defaultMaterial);
        _renderer.material = _cachedMaterial;
        _cachedMaterial.color = _defaultColor;
        
    }

    private void DrawIndexText(string indexText)
    {
        foreach (var text in _texts)
        {
            text.text = indexText;
        }
    }

    private void OnDestroy()
    {
        if (_cachedMaterial != null)
        {
            Destroy(_cachedMaterial);
        }
    }
}