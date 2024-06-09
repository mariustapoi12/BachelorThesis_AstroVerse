using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class PlanetEditorInitialization
{
    static PlanetEditorInitialization()
    {
        EditorApplication.update += OnEditorUpdate;
    }

    private static void OnEditorUpdate()
    {
        // Find all Planet objects in the scene
        Planet[] planets = GameObject.FindObjectsOfType<Planet>();
        foreach (Planet planet in planets)
        {
            planet.GeneratePlanet();
        }
        // Remove the update method to avoid continuous execution
        EditorApplication.update -= OnEditorUpdate;
    }
}
