using UnityEngine;

[CreateAssetMenu]
public class ColorSettings : ScriptableObject
{
    public Material planetMaterial;
    public BiomeColorSettings biomeColorSettings;
    public Gradient oceanColor;

    [System.Serializable]
    public class BiomeColorSettings
    {
        public Biome[] biomes;
        public NoiseSettings noiseSettings;
        public float noiseOffset;
        public float noiseStrength;
        [Range(0f, 1f)]
        public float blendedIntensity;

        [System.Serializable]
        public class Biome
        {
            public Gradient gradient;
            public Color tint;
            [Range(0f, 1f)]
            public float startHeight;
            [Range (0f, 1f)]
            public float tintPercent;
        }
    }
}
