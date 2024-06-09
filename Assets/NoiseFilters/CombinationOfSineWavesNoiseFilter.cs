using UnityEngine;

public class CombinationOfSineWavesNoiseFilter : INoiseFilter
{
    private readonly INoise noise;

    private readonly NoiseSettings.ParametrizedNoiseSettings settings;

    public CombinationOfSineWavesNoiseFilter(NoiseSettings.ParametrizedNoiseSettings settings, INoise noise)
    {
        this.settings = settings;
        this.noise = noise;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.octaves; i++)
        {
            point = point * frequency + settings.centre;
            float sinX = noise.GetValue(point.x, point.y, point.z);
            float sin2X = noise.GetValue(2 * point.x, 2 * point.y, 2 * point.z);
            float sin3X = noise.GetValue(3 * point.x, 3 * point.y, 3 * point.z);
            float sumOfSines = sinX + sin2X + sin3X;
            float noisePoint = 2.5f - Mathf.Abs(sumOfSines);
            noisePoint *= noisePoint;
            noisePoint *= weight;
            weight = Mathf.Clamp01(settings.weightMultiplier * noisePoint);

            noiseValue += noisePoint * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        noiseValue -= settings.minValue;
        return noiseValue * settings.strength;
    }
}
