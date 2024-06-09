public static class NoiseFactory
{
    public static INoise SetNoise(NoiseSettings noiseSettings)
    {
        INoise noise = noiseSettings.noise switch
        {
            NoiseSettings.Noise.Perlin => new PerlinNoise(),
            NoiseSettings.Noise.Value => new ValueNoise(),
            NoiseSettings.Noise.Simplex => new SimplexNoise(),
            NoiseSettings.Noise.Cubic => new CubicNoise(),
            _ => null
        };

        noise?.SetSeed(noiseSettings.Seed);

        return noise;
    }
}
