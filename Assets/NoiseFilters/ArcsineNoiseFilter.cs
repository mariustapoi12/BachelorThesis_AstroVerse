using UnityEngine;

public class ArcSineNoiseFilter : INoiseFilter
{
    private readonly INoise noise;

    private readonly NoiseSettings.ParametrizedNoiseSettings settings;

    public ArcSineNoiseFilter(NoiseSettings.ParametrizedNoiseSettings settings, INoise noise)
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
            //Math.Abs(arcsinSinX);
            float noisePoint = Mathf.Abs(Mathf.Asin(noise.GetValue(point.x, point.y, point.z)));
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