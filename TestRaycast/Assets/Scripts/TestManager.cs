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
    private RaycastObjectData[] _raycastObjectDatas;

    private void Start()
    {
        SpawnCubes();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SpawnCubes()
    {
        _spawnedCubes = new Cube[_cubeCount];
        _raycastObjectDatas = new RaycastObjectData[_cubeCount];

        for (int i = 0; i < _cubeCount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * _spawnRadius + _boxSpawnCenter.position;
            Cube spawnedCube = Instantiate(_cubePrefab, pos, Quaternion.identity);

            _spawnedCubes[i] = spawnedCube;
            _raycastObjectDatas[i] = new RaycastObjectData(pos, 1);
        }
    }

    private void NewShootRay()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastEngine.UpdateHitData(origin, direction, _rayLength, _raycastObjectDatas);


        for (var index = 0; index < _raycastObjectDatas.Length; index++)
        {
            var raycastObjectData = _raycastObjectDatas[index];
            if (raycastObjectData.HitStatus)
            {
                _spawnedCubes[index].ChangeHitColor();
            }
            else
            {
                _spawnedCubes[index].ChangeDefaultColor();
            }
        }
    }


    private void Update()
    {
        NewShootRay();
    }
}