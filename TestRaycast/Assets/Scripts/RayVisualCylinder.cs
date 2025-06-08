using System;
using UnityEngine;

public class RayVisualCylinder : MonoBehaviour
{
    [SerializeField] private TestManager _testManager;
    
    private void Start()
    {
        Vector3 scale = transform.localScale;
        scale.y = _testManager.RayLength;
        transform.localScale = scale;
        transform.localPosition = transform.up * _testManager.RayLength;
    }
}