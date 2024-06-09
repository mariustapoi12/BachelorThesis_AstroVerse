using UnityEngine;

public class CubicNoise : INoise
{
    private int seed;
    public float GetValue(Vector3 point)
    {
        return GetValue(point.x, point.y, point.z);
    }

    public void SetSeed(int seed)
    {
        this.seed = seed;
    }

    public float GetValue(float x, float y, float z)
    {
        int x1 = Mathf.FloorToInt(x);
        int y1 = Mathf.FloorToInt(y);
        int z1 = Mathf.FloorToInt(z);

        float xs = (float)(x - x1);
        float ys = (float)(y - y1);
        float zs = (float)(z - z1);

        int x0 = x1 - 1;
        int y0 = y1 - 1;
        int z0 = z1 - 1;
        int x2 = x1 + 1;
        int y2 = y1 + 1;
        int z2 = z1 + 1;
        int x3 = x1 + 2;
        int y3 = y1 + 2;
        int z3 = z1 + 2;
        return CubicInterpolation(CubicInterpolation(
            CubicInterpolation(GenerateRandomValue(seed, x0, y0, z0), GenerateRandomValue(seed, x1, y0, z0), GenerateRandomValue(seed, x2, y0, z0), GenerateRandomValue(seed, x3, y0, z0), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y1, z0), GenerateRandomValue(seed, x1, y1, z0), GenerateRandomValue(seed, x2, y1, z0), GenerateRandomValue(seed, x3, y1, z0), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y2, z0), GenerateRandomValue(seed, x1, y2, z0), GenerateRandomValue(seed, x2, y2, z0), GenerateRandomValue(seed, x3, y2, z0), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y3, z0), GenerateRandomValue(seed, x1, y3, z0), GenerateRandomValue(seed, x2, y3, z0), GenerateRandomValue(seed, x3, y3, z0), xs),
            ys),
            CubicInterpolation(
            CubicInterpolation(GenerateRandomValue(seed, x0, y0, z1), GenerateRandomValue(seed, x1, y0, z1), GenerateRandomValue(seed, x2, y0, z1), GenerateRandomValue(seed, x3, y0, z1), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y1, z1), GenerateRandomValue(seed, x1, y1, z1), GenerateRandomValue(seed, x2, y1, z1), GenerateRandomValue(seed, x3, y1, z1), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y2, z1), GenerateRandomValue(seed, x1, y2, z1), GenerateRandomValue(seed, x2, y2, z1), GenerateRandomValue(seed, x3, y2, z1), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y3, z1), GenerateRandomValue(seed, x1, y3, z1), GenerateRandomValue(seed, x2, y3, z1), GenerateRandomValue(seed, x3, y3, z1), xs),
            ys),
            CubicInterpolation(
            CubicInterpolation(GenerateRandomValue(seed, x0, y0, z2), GenerateRandomValue(seed, x1, y0, z2), GenerateRandomValue(seed, x2, y0, z2), GenerateRandomValue(seed, x3, y0, z2), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y1, z2), GenerateRandomValue(seed, x1, y1, z2), GenerateRandomValue(seed, x2, y1, z2), GenerateRandomValue(seed, x3, y1, z2), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y2, z2), GenerateRandomValue(seed, x1, y2, z2), GenerateRandomValue(seed, x2, y2, z2), GenerateRandomValue(seed, x3, y2, z2), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y3, z2), GenerateRandomValue(seed, x1, y3, z2), GenerateRandomValue(seed, x2, y3, z2), GenerateRandomValue(seed, x3, y3, z2), xs),
            ys),
            CubicInterpolation(
            CubicInterpolation(GenerateRandomValue(seed, x0, y0, z3), GenerateRandomValue(seed, x1, y0, z3), GenerateRandomValue(seed, x2, y0, z3), GenerateRandomValue(seed, x3, y0, z3), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y1, z3), GenerateRandomValue(seed, x1, y1, z3), GenerateRandomValue(seed, x2, y1, z3), GenerateRandomValue(seed, x3, y1, z3), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y2, z3), GenerateRandomValue(seed, x1, y2, z3), GenerateRandomValue(seed, x2, y2, z3), GenerateRandomValue(seed, x3, y2, z3), xs),
            CubicInterpolation(GenerateRandomValue(seed, x0, y3, z3), GenerateRandomValue(seed, x1, y3, z3), GenerateRandomValue(seed, x2, y3, z3), GenerateRandomValue(seed, x3, y3, z3), xs),
            ys),
            zs);
    }

    private static float GenerateRandomValue(int seed, int x, int y, int z)
    {
        int hash = seed ^ x ^ y ^ z;
        hash *= 0x27d4eb2d;
        hash *= hash;
        hash ^= (hash << 18);

        return hash / (float)int.MaxValue;
    }

    private static float CubicInterpolation(float a, float b, float c, float d, float t)
    {
        float p = (d - c) - (a - b);
        return t * t * t * p + t * t * ((a - b) - p) + t * (c - a) + b;
    }

}
