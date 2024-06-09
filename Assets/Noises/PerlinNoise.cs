using TreeEditor;
using UnityEngine;

public class PerlinNoise : INoise
{
    readonly Perlin noise = new();

    public float GetValue(Vector3 point)
    {
        return noise.Noise(point.x, point.y, point.z);
    }

    public void SetSeed(int seed)
    {
        noise.SetSeed(seed);
    }
    public float GetValue(float x, float y, float z)
    {
        return noise.Noise(x, y, z);
    }
}
