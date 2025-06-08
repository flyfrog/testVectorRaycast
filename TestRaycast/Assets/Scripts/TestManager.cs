using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestManager : MonoBehaviour
{
    public float RayLength => _rayLength;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _cubeCount = 20;
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private Transform _boxSpawnCenter;
    [SerializeField] private float _rayLength = 50f;
    
    private Cube[] _spawnedCubes;
    private int _lastHitIndex = -1;

    void Start()
    {
        SpawnCubes();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void SpawnCubes()
    {
        _spawnedCubes = new Cube[_cubeCount];
        
        for (int i = 0; i < _cubeCount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * _spawnRadius + _boxSpawnCenter.position;
            Cube spawnedCube = Instantiate(_cubePrefab, pos, Quaternion.identity);


            _spawnedCubes[i] = spawnedCube;
        }
    }


    void ShootRay()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        List<int> hits = new();

        for (int i = 0; i < _spawnedCubes.Length; i++)
        {
            Vector3 relativePos = _spawnedCubes[i].Position - origin;

            if (RaycastEngine.RaycastIntersectsCube(origin, direction, relativePos, _rayLength))
            {
                hits.Add((i));
            }
        }


        HashSet<int> hitIndices = new HashSet<int>();
        foreach (var hit in hits)
        {
            hitIndices.Add(hit);
        }

        for (int i = 0; i < _spawnedCubes.Length; i++)
        {
            if (hitIndices.Contains(i))
            {
                _spawnedCubes[i].ChangeHitColor();
            }
            else
            {
                _spawnedCubes[i].ChangeDefaultColor();
            }
        }
    }


    private void Update()
    {
        ShootRay();
    }
}