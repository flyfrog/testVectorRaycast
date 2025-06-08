using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Настройки чувствительности")]
    public float sensitivityX = 100f;
    public float sensitivityY = 100f;

    [Header("Ограничение по вертикали")]
    public float minY = -90f;
    public float maxY = 90f;

    private float rotationY = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        // Обновляем вертикальный угол (вверх/вниз) и ограничиваем его
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // Вращаем объект по вертикали и горизонтали
        transform.localEulerAngles = new Vector3(rotationY, transform.localEulerAngles.y + mouseX, 0f);
    }
}