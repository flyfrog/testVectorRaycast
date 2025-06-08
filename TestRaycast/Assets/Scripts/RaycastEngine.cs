using UnityEngine;

public static class RaycastEngine
{
    private const float EPSILON = 0.000001f;

    public static void UpdateHitData(Vector3 raycastStartPosition, Vector3 direction, float rayLength,
          RaycastObjectData[] raycastObjectDatas)
    {
        for (int i = 0; i < raycastObjectDatas.Length; i++)
        {
            Vector3 relativePos = raycastObjectDatas[i].Position - raycastStartPosition;

            if (RaycastIntersectsCube(raycastStartPosition, direction, relativePos, rayLength))
            {
                raycastObjectDatas[i].SetHitStatus(true);
            }
            else
            {
                raycastObjectDatas[i].SetHitStatus(false);
            }
        }
    }

    public static bool RaycastIntersectsCube(Vector3 raycastStartPosition, Vector3 direction, Vector3 cubeCenter,
        float rayDistance)
    {
        Vector3 min = cubeCenter - Vector3.one * 0.5f;
        Vector3 max = cubeCenter + Vector3.one * 0.5f;

        float tMin = float.MinValue;
        float tMax = float.MaxValue;

        for (int i = 0; i < 3; i++)
        {
            float o = raycastStartPosition[i];
            float d = direction[i];
            float mn = min[i];
            float mx = max[i];

            if (Mathf.Abs(d) < EPSILON)
            {
                if (o < mn || o > mx)
                {
                    return false;
                }
            }
            else
            {
                float t1 = (mn - o) / d;
                float t2 = (mx - o) / d;

                if (t1 > t2)
                {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
                }

                tMin = Mathf.Max(tMin, t1);
                tMax = Mathf.Min(tMax, t2);

                if (tMin > tMax)
                {
                    return false;
                }
            }
        }

        return tMin >= 0f && tMin <= rayDistance;
    }
}

public class RaycastObjectData
{
    public bool HitStatus { get; private set; }
    public Vector3 Position { get; private set; }
    public float Size { get; private set; }

    public RaycastObjectData(Vector3 position, float size)
    {
        Position = position;
        Size = size;
    }

    public void SetHitStatus(bool status)
    {
        HitStatus = status;
    }
}