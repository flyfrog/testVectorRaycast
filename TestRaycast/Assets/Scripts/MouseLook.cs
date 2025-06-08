using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Настройки чувствительности")] 
    [SerializeField] private float _sensitivityX = 100f;
    [SerializeField] private float _sensitivityY = 100f;

    [Header("Ограничение по вертикали")] 
    [SerializeField] private float _minY = -90f;
    [SerializeField] private float _maxY = 90f;

    private float _rotationY = 0f;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivityY * Time.deltaTime;

        _rotationY -= mouseY;
        _rotationY = Mathf.Clamp(_rotationY, _minY, _maxY);
        transform.localEulerAngles = new Vector3(_rotationY, transform.localEulerAngles.y + mouseX, 0f);
    }
}