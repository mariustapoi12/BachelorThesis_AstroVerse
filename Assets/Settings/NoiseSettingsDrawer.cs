using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NoiseSettings))]
public class NoiseSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var noiseProperty = property.FindPropertyRelative("noise");
        var seedProperty = property.FindPropertyRelative("Seed");
        var filterTypeProperty = property.FindPropertyRelative("filterType");
        var simpleNoiseSettingsProperty = property.FindPropertyRelative("simpleNoiseSettings");
        var parametrizedNoiseSettingsProperty = property.FindPropertyRelative("parameterizedNoiseSettings");

        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(position, noiseProperty);
        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField(position, seedProperty);

        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        if (GUI.Button(position, "Random Seed"))
        {
            seedProperty.intValue = Random.Range(int.MinValue, int.MaxValue);
        }

        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField(position, filterTypeProperty);

        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        if (filterTypeProperty.enumValueIndex == (int)NoiseSettings.FilterType.Simple)
        {
            DrawSimpleNoiseSettings(ref position, simpleNoiseSettingsProperty);
        }
        else
        {
            DrawParametrizedNoiseSettings(ref position, parametrizedNoiseSettingsProperty);
        }

        EditorGUI.EndProperty();
    }

    private void DrawSimpleNoiseSettings(ref Rect position, SerializedProperty simpleNoiseSettingsProperty)
    {
        DrawProperty(ref position, simpleNoiseSettingsProperty.FindPropertyRelative("strength"));
        DrawProperty(ref position, simpleNoiseSettingsProperty.FindPropertyRelative("octaves"));
        DrawProperty(ref position, simpleNoiseSettingsProperty.FindPropertyRelative("persistence"));
        DrawProperty(ref position, simpleNoiseSettingsProperty.FindPropertyRelative("baseRoughness"));
        DrawProperty(ref position, simpleNoiseSettingsProperty.FindPropertyRelative("roughness"));
        DrawProperty(ref position, simpleNoiseSettingsProperty.FindPropertyRelative("centre"));
        DrawProperty(ref position, simpleNoiseSettingsProperty.FindPropertyRelative("minValue"));
    }

    private void DrawParametrizedNoiseSettings(ref Rect position, SerializedProperty parametrizedNoiseSettingsProperty)
    {
        DrawSimpleNoiseSettings(ref position, parametrizedNoiseSettingsProperty);
        DrawProperty(ref position, parametrizedNoiseSettingsProperty.FindPropertyRelative("weightMultiplier"));
    }

    private void DrawProperty(ref Rect position, SerializedProperty property)
    {
        EditorGUI.PropertyField(position, property);
        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight * 4 + EditorGUIUtility.standardVerticalSpacing * 3;

        var filterTypeProperty = property.FindPropertyRelative("filterType");
        if (filterTypeProperty.enumValueIndex == (int)NoiseSettings.FilterType.Simple)
        {
            height += EditorGUIUtility.singleLineHeight * 7.2f + EditorGUIUtility.standardVerticalSpacing * 6;
        }
        else
        {
            height += EditorGUIUtility.singleLineHeight * 8.2f + EditorGUIUtility.standardVerticalSpacing * 7;
        }

        return height;
    }
}
