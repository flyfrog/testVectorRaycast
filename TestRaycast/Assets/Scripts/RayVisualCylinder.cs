using System;
using UnityEngine;

public class RayVisualCylinder : MonoBehaviour
{
    [Header("Длина луча (по оси Y)")] 
    
    public float length = 5f;

    [SerializeField] private TestManager _testManager;
    
    private void Start()
    {
        Vector3 scale = transform.localScale;
        scale.y = length;
        transform.localScale = scale;
        transform.localPosition = transform.up * _testManager.RayLength;
    }
}