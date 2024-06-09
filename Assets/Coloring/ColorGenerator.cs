using System;
using Unity.VisualScripting;
using UnityEngine;

public class ColorGenerator
{
    ColorSettings colorSettings;
    Texture2D texture;
    const int textureResolution = 50;
    INoiseFilter biomeNoiseFilter;
    INoise biomeNoise;

    public void UpdateSettings(ColorSettings colorSettings)
    {
        this.colorSettings = colorSettings;
        int biomesLength = colorSettings.biomeColorSettings.biomes.Length;

        if (texture == null || texture.height != biomesLength)
        {
            texture = new Texture2D(textureResolution * 2, biomesLength, TextureFormat.RGBA32, false);
        }

        biomeNoise = NoiseFactory.SetNoise(colorSettings.biomeColorSettings.noiseSettings);
        biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(colorSettings.biomeColorSettings.noiseSettings, biomeNoise);
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        colorSettings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public float BiomePercentFromPoint(Vector3 point)
    {
        float heightPercent = (point.y + 1) / 2f;
        heightPercent += (biomeNoiseFilter.Evaluate(point) - colorSettings.biomeColorSettings.noiseOffset) * colorSettings.biomeColorSettings.noiseStrength;

        float biomeIndex = 0;
        int numberOfBiomes = colorSettings.biomeColorSettings.biomes.Length;
        float blendRange = colorSettings.biomeColorSettings.blendedIntensity / 2f + 0.001f;

        foreach (var biome in colorSettings.biomeColorSettings.biomes)
        {
            float distance = heightPercent - biome.startHeight;
            float weight = Mathf.InverseLerp(-blendRange, blendRange, distance);
            biomeIndex = biomeIndex * (1 - weight) + weight * Array.IndexOf(colorSettings.biomeColorSettings.biomes, biome);
        }

        return biomeIndex / Mathf.Max(1, numberOfBiomes - 1);
    }

    public void UpdateColors()
    {
        Color[] colors = new Color[texture.width * texture.height];
        int index = 0;

        foreach (var biome in colorSettings.biomeColorSettings.biomes)
        {
            for (int i = 0; i < textureResolution * 2; i++)
            {
                Color gradientColor = i < textureResolution
                    ? colorSettings.oceanColor.Evaluate(i / (textureResolution - 1f))
                    : biome.gradient.Evaluate((i - textureResolution) / (textureResolution - 1f));

                colors[index++] = Color.Lerp(gradientColor, biome.tint, biome.tintPercent);
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        colorSettings.planetMaterial.SetTexture("_planetTexture", texture);
    }
}
