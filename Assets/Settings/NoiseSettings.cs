using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public enum Noise { Perlin, Simplex, Value, Cubic }
    public Noise noise;

    public int Seed = 0;
    public enum FilterType { Simple, Sine, SineSquared, Exponential, CombinationOfSineWaves, SawtoothWave, Arcsine }
    public FilterType filterType;

    public SimpleNoiseSettings simpleNoiseSettings = new SimpleNoiseSettings();
    public ParametrizedNoiseSettings parameterizedNoiseSettings = new ParametrizedNoiseSettings();

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public float strength = 1;
        [Range(1, 8)]
        public int octaves = 1;
        public float persistence = .5f;
        public float baseRoughness = 1;
        public float roughness = 2;
        public Vector3 centre;
        public float minValue;
    }

    [System.Serializable]
    public class ParametrizedNoiseSettings : SimpleNoiseSettings
    {
        public float weightMultiplier = .8f;
    }
}
