using UnityEngine;

public class NoiseFilter: INoiseFilter
{
    private readonly INoise noise;

    private readonly NoiseSettings.SimpleNoiseSettings settings;

    public NoiseFilter(NoiseSettings.SimpleNoiseSettings settings, INoise noise)
    {
        this.settings = settings;
        this.noise = noise;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < settings.octaves; i++)
        {
            point = point * frequency + settings.centre;
            float noisePoint = noise.GetValue(point);
            noiseValue += (noisePoint + 1) * .5f * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        noiseValue -= settings.minValue;
        return noiseValue * settings.strength;
    }
}
