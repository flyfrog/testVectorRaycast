using UnityEngine;

public static class RaycastEngine
{
    public static void UpdateHitData(Vector3 raycastStartPosition, Vector3 direction, float rayLength,
        RaycastObjectData[] raycastObjectDatas)
    {
        for (int i = 0; i < raycastObjectDatas.Length; i++)
        {
            if (RaycastIntersectsCube(raycastStartPosition, direction, raycastObjectDatas[i].Position, rayLength,
                    raycastObjectDatas[i].Size))
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
        Vector3 cubeMin = cubeCenter - size / 2;
        Vector3 cubeMax = cubeCenter + size / 2;
        
        float tmin = float.NegativeInfinity;
        float tmax = float.PositiveInfinity;

        for (int i = 0; i < 3; i++)
        {
            if (Mathf.Approximately(direction[i], 0f))
            {
                if (rayStartPosition[i] < cubeMin[i] || rayStartPosition.x > cubeMax[i])
                {
                    return false;
                }
            }
            else
            {
                float t1 = (cubeMin[i] - rayStartPosition[i]) / direction[i];
                float t2 = (cubeMax[i] - rayStartPosition[i]) / direction[i];
                
                if (t1 > t2)
                {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
                }
                
                tmin = Mathf.Max(tmin, t1);
                tmax = Mathf.Min(tmax, t2);
                
                if (tmin > tmax) return false;
            }
        }
        
        return tmin <= tmax && tmax >= 0 && tmin <= rayDistance;
    }
}