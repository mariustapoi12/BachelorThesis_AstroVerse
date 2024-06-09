using UnityEngine;

public static class NoiseFilterFactory
{
    public static INoiseFilter CreateNoiseFilter(NoiseSettings noiseSettings, INoise noise)
    {
        return noiseSettings.filterType switch
        {
            NoiseSettings.FilterType.Simple => new NoiseFilter(noiseSettings.simpleNoiseSettings, noise),
            NoiseSettings.FilterType.Sine => new SineNoiseFilter(noiseSettings.parameterizedNoiseSettings, noise),
            NoiseSettings.FilterType.SineSquared => new SineSquaredNoiseFilter(noiseSettings.parameterizedNoiseSettings, noise),
            NoiseSettings.FilterType.Exponential => new ExponentialNoiseFilter(noiseSettings.parameterizedNoiseSettings, noise),
            NoiseSettings.FilterType.CombinationOfSineWaves => new CombinationOfSineWavesNoiseFilter(noiseSettings.parameterizedNoiseSettings, noise),
            NoiseSettings.FilterType.SawtoothWave => new SawtoothWaveNoiseFilter(noiseSettings.parameterizedNoiseSettings, noise),
            NoiseSettings.FilterType.Arcsine => new ArcSineNoiseFilter(noiseSettings.parameterizedNoiseSettings, noise),
            _ => null
        };
    }
}
