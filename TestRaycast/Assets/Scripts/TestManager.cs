using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestManager : MonoBehaviour
{
    public event Action<RaycastObjectData[]> HitDataUpdatedEvent;
    public float RayLength => _rayLength;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _cubeCount = 20;
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private Transform _boxSpawnCenter;
    [SerializeField] private float _rayLength = 50f;

    private Cube[] _spawnedCubes;
    private RaycastObjectData[] _raycastObjectData;

    private void Start()
    {
        SpawnCubes();
    }

    private void SpawnCubes()
    {
        _spawnedCubes = new Cube[_cubeCount];
        _raycastObjectData = new RaycastObjectData[_cubeCount];

        for (int i = 0; i < _cubeCount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * _spawnRadius + _boxSpawnCenter.position;
            Cube spawnedCube = Instantiate(_cubePrefab, pos, Quaternion.identity);
            spawnedCube.Construct(i);
            _spawnedCubes[i] = spawnedCube;
            _raycastObjectData[i] = new RaycastObjectData(pos, Vector3.one);
        }
    }

    private void ShootRay()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastEngine.UpdateHitData(origin, direction, _rayLength, _raycastObjectData);
        
        for (var index = 0; index < _raycastObjectData.Length; index++)
        {
            var raycastObjectData = _raycastObjectData[index];
            if (raycastObjectData.HitStatus)
            {
                _spawnedCubes[index].SetHitColor();
            }
            else
            {
                _spawnedCubes[index].SetDefaultColor();
            }
        }

        HitDataUpdatedEvent?.Invoke(_raycastObjectData);
    }
    
    private void Update()
    {
        ShootRay();
    }
}