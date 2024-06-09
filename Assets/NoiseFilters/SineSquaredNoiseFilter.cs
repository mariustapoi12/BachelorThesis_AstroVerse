using UnityEngine;

public class SineSquaredNoiseFilter : INoiseFilter
{
    private readonly INoise noise;

    private readonly NoiseSettings.ParametrizedNoiseSettings settings;

    public SineSquaredNoiseFilter(NoiseSettings.ParametrizedNoiseSettings settings, INoise noise)
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
            //Math.Pow(1 - Math.Abs(Math.Sin(x)), 2);
            float noisePoint = Mathf.Pow(1 - Mathf.Abs(noise.GetValue(point)), 2);
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
