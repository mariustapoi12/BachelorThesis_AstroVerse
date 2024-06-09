using UnityEngine;

public class ExponentialNoiseFilter : INoiseFilter
{
    private readonly INoise noise;

    private readonly NoiseSettings.ParametrizedNoiseSettings settings;

    public ExponentialNoiseFilter(NoiseSettings.ParametrizedNoiseSettings settings, INoise noise)
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
            //Math.Exp(-Math.Abs(Math.Sin(x))) - 0.35;
            float noisePoint = Mathf.Exp(-Mathf.Abs(noise.GetValue(point))) - 0.35f;
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
