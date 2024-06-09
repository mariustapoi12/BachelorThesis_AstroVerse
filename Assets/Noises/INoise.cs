using UnityEngine;

public interface INoise
{
    float GetValue(Vector3 point);

    void SetSeed(int seed);

    float GetValue(float x, float y, float z);
}
