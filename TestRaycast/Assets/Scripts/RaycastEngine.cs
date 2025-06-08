using UnityEngine;

public static class RaycastEngine
{
    private const float EPSILON = 0.000001f;

    public static void UpdateHitData(Vector3 raycastStartPosition, Vector3 direction, float rayLength,
          RaycastObjectData[] raycastObjectDatas)
    {
        for (int i = 0; i < raycastObjectDatas.Length; i++)
        {
            // Vector3 relativePos = raycastObjectDatas[i].Position - raycastStartPosition;

            if (RaycastIntersectsCube(raycastStartPosition, direction, raycastObjectDatas[i].Position, rayLength, raycastObjectDatas[i].Size))
            {
                raycastObjectDatas[i].SetHitStatus(true);
            }
            else
            {
                raycastObjectDatas[i].SetHitStatus(false);
            }
        }
    }

    public static bool RaycastIntersectsCube(Vector3 rayStartPosition, Vector3 direction, Vector3 cubeCenter,
        float rayDistance, Vector3 size)
    {
         // Вычисляем минимальные и максимальные координаты углов куба
        Vector3 cubeMin = cubeCenter - size / 2;
        Vector3 cubeMax = cubeCenter + size / 2;

        // Инициализируем минимальное и максимальное "время" пересечения
        float tmin = float.NegativeInfinity;
        float tmax = float.PositiveInfinity;
        
        // --- Проверка по оси X ---
        if (Mathf.Approximately(direction.x, 0f))
        {
            // Если луч параллелен оси и находится вне "слоя" куба, пересечения нет
            if (rayStartPosition.x < cubeMin.x || rayStartPosition.x > cubeMax.x)
            {
                return false;
            }
        }
        else
        {
            // Рассчитываем время входа и выхода из "слоя" по оси X
            float t1 = (cubeMin.x - rayStartPosition.x) / direction.x;
            float t2 = (cubeMax.x - rayStartPosition.x) / direction.x;

            // Убеждаемся, что t1 - это время входа (меньшее)
            if (t1 > t2) {
                float temp = t1;
                t1 = t2;
                t2 = temp;
            }
            
            // Обновляем общий интервал пересечения
            tmin = Mathf.Max(tmin, t1);
            tmax = Mathf.Min(tmax, t2);
            
            // Если интервал "схлопнулся", пересечения нет
            if (tmin > tmax) return false;
        }

        // --- Проверка по оси Y ---
        if (Mathf.Approximately(direction.y, 0f))
        {
            if (rayStartPosition.y < cubeMin.y || rayStartPosition.y > cubeMax.y)
            {
                return false;
            }
        }
        else
        {
            float t1 = (cubeMin.y - rayStartPosition.y) / direction.y;
            float t2 = (cubeMax.y - rayStartPosition.y) / direction.y;

            if (t1 > t2) {
                float temp = t1;
                t1 = t2;
                t2 = temp;
            }
            
            tmin = Mathf.Max(tmin, t1);
            tmax = Mathf.Min(tmax, t2);

            if (tmin > tmax) return false;
        }
        
        // --- Проверка по оси Z ---
        if (Mathf.Approximately(direction.z, 0f))
        {
            if (rayStartPosition.z < cubeMin.z || rayStartPosition.z > cubeMax.z)
            {
                return false;
            }
        }
        else
        {
            float t1 = (cubeMin.z - rayStartPosition.z) / direction.z;
            float t2 = (cubeMax.z - rayStartPosition.z) / direction.z;

            if (t1 > t2) {
                float temp = t1;
                t1 = t2;
                t2 = temp;
            }

            tmin = Mathf.Max(tmin, t1);
            tmax = Mathf.Min(tmax, t2);

            if (tmin > tmax) return false;
        }
        
        // Финальная проверка:
        // 1. Интервал пересечения существует (tmin <= tmax)
        // 2. Пересечение находится впереди луча (tmax >= 0)
        // 3. Ближайшая точка пересечения находится в пределах дистанции луча (tmin <= rayDistance)
        return tmin <= tmax && tmax >= 0 && tmin <= rayDistance;
    }
}