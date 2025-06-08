using UnityEngine;

public class Cube : MonoBehaviour
{
    public Vector3 Position => transform.position;
    
    [Header("Materials")] 
    [SerializeField] private Material _defaultMaterial; // Красный
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _hitColor;
    private Material _cachedMaterial;

    private void Start()
    {
        _cachedMaterial = new Material(_defaultMaterial);
        _renderer.material = _cachedMaterial;
        _cachedMaterial.color = _defaultColor;
    }
    
    void OnDestroy()
    {
        if (_cachedMaterial != null)
        {
            Destroy(_cachedMaterial);
        }
    }

    public void ChangeHitColor()
    {
        _cachedMaterial.color = _hitColor;
    }

    public void ChangeDefaultColor()
    {
        _cachedMaterial.color = _defaultColor;
    }
    
}