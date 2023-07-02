using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    MeshFilter mf;
    private Mesh mesh;
    private Vector3[] vertices = new Vector3[4];
    private Color[] colors = new Color[4];
    private int[] triangles = new int[6];
    [SerializeField]
    int row = 8;
    [SerializeField]
    int column = 6;
    [SerializeField]
    private float width = 2;
    [SerializeField]
    private float height = 2;
    [SerializeField]
    private float skinWidth = 0.1f;
    [SerializeField]
    private float max;
    [SerializeField]
    AudioSource audioSource;
    int resolution = 1024;
    Vector3[] postions = new Vector3[1024];
    [SerializeField]
    float gain = 10f;
    [SerializeField]
    float raidus = 1f;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out mf);
        mesh = new Mesh();
        vertices = new Vector3[4 * row * column];
        triangles = new int[6 * row * column];
        colors = new Color[4 * row * column];
        int vIndex = 0;
        int tIndex = 0;
        for (int j = 0; j < column; j++)
        {
            for (int i = 0; i < row; i++)
            {
                Vector3 ll = new Vector3(j * width, i * height, 0);
                vertices[vIndex + 0] = ll + new Vector3(skinWidth, skinWidth, 0);
                vertices[vIndex + 1] = ll + new Vector3(width - skinWidth, skinWidth, 0);
                vertices[vIndex + 2] = ll + new Vector3(skinWidth, height - skinWidth, 0);
                vertices[vIndex + 3] = ll + new Vector3(width - skinWidth, height - skinWidth, 0);
                colors[vIndex + 0] = Color.white;
                colors[vIndex + 1] = Color.white;
                colors[vIndex + 2] = Color.white;
                colors[vIndex + 3] = Color.white;
                triangles[tIndex + 0] = vIndex;
                triangles[tIndex + 1] = vIndex + 2;
                triangles[tIndex + 2] = vIndex + 1;
                triangles[tIndex + 3] = vIndex + 2;
                triangles[tIndex + 4] = vIndex + 3;
                triangles[tIndex + 5] = vIndex + 1;
                vIndex += 4;
                tIndex += 6;
            }
        }


        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetColors(colors);
        mf.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = new float[resolution];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        float[] spectrumSum = new float[column];
        for (int i = 0; i < resolution; i++)
        {
            spectrumSum[column * i / resolution] += spectrum[i] * gain;
        }
        int cIndex = 0;
        for (int j = 0; j < column; j++)
        {
            for (int i = 0; i < row; i++)
            {
                if (spectrumSum[j] >= max * i / row)
                {
                    colors[cIndex + 0] = Color.white;
                    colors[cIndex + 1] = Color.white;
                    colors[cIndex + 2] = Color.white;
                    colors[cIndex + 3] = Color.white;
                }
                else
                {
                    /*
                    colors[cIndex + 0] = Color.clear;
                    colors[cIndex + 1] = Color.clear;
                    colors[cIndex + 2] = Color.clear;
                    colors[cIndex + 3] = Color.clear;
                    */
                    colors[cIndex + 0] *= 0.5f;
                    colors[cIndex + 1] *= 0.5f;
                    colors[cIndex + 2] *= 0.5f;
                    colors[cIndex + 3] *= 0.5f;
                }
                cIndex += 4;
            }
        }

        mesh.SetColors(colors);
        mf.mesh = mesh;
    }
}
