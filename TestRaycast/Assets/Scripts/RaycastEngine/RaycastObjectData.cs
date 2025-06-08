using UnityEngine;

public class RaycastObjectData
{
    public bool HitStatus { get; private set; }
    public Vector3 Position { get; private set; }
    public Vector3 Size { get; private set; }

    public RaycastObjectData(Vector3 position, Vector3 size)
    {
        Position = position;
        Size = size;
    }

    public void SetHitStatus(bool status)
    {
        HitStatus = status;
    }
}