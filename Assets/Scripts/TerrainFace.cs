using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 X;
    Vector3 Y;
    ShapeGenerator shapeGenerator;

    public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        X = new Vector3(localUp.y, localUp.z, localUp.x);
        Y = Vector3.Cross(localUp, X);
    }

    public void CreateMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution-1) * (resolution-1) * 6];
        int triangleIndex = 0;
        Vector2[] uv = (mesh.uv.Length == vertices.Length) ? mesh.uv : new Vector2[vertices.Length];
        int i = 0;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                Vector2 percent = new Vector2(x, y) / (resolution-1);
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * X + (percent.y - 0.5f) * 2 * Y;
                Vector3 pointOnUnitSpehere = pointOnUnitCube.normalized;
                float unscaledElevation = shapeGenerator.CalculateUnscaledElevation(pointOnUnitSpehere);
                vertices[i] = pointOnUnitSpehere * shapeGenerator.GetScaledElevation(unscaledElevation);
                uv[i].y = unscaledElevation;

                if (x != resolution -1 && y != resolution -1)
                {
                    triangles[triangleIndex] = i;
                    triangles[triangleIndex+1] = i+resolution+1;
                    triangles[triangleIndex+2] = i+resolution;

                    triangles[triangleIndex+3] = i;
                    triangles[triangleIndex + 4] = i + 1;
                    triangles[triangleIndex + 5] = i + resolution + 1;

                    triangleIndex += 6;
                }
                i++;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uv;
    }

    public void UpdateUVs(ColorGenerator colorGenerator)
    {
        Vector2[] uv = mesh.uv;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * X + (percent.y - 0.5f) * 2 * Y;
                Vector3 pointOnUnitSpehere = pointOnUnitCube.normalized;

                uv[i].x = colorGenerator.BiomePercentFromPoint(pointOnUnitSpehere);
            }
        }
        mesh.uv = uv;
    }
}
