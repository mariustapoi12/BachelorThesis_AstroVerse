using LibNoise.Primitive;
using UnityEngine;

public class SimplexNoise : INoise
{
    readonly SimplexPerlin noise = new();

    public float GetValue(Vector3 point)
    {
        return noise.GetValue(point.x, point.y, point.z);
    }

    public void SetSeed(int seed)
    {
        noise.Seed = seed;
    }
    public float GetValue(float x, float y, float z)
    {
        return noise.GetValue(x, y, z);
    }
}
