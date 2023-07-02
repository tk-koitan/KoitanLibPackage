using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class VisualizerCircle : MonoBehaviour
{
    MeshFilter mf;
    [SerializeField]
    int sortingOrder;
    private Mesh mesh;
    private Vector3[] vertices = new Vector3[4];
    private Color[] colors = new Color[4];
    private int[] triangles = new int[6];
    [SerializeField]
    int row = 8;
    [SerializeField]
    int column = 6;
    [SerializeField]
    private float skinAngle = 1f;
    [SerializeField]
    private float skinWidth = 0.1f;
    [SerializeField]
    private float max;
    [SerializeField]
    AudioSource audioSource;
    int resolution = 1024;
    [SerializeField]
    float raidus = 1f;
    [SerializeField]
    float attenuationRate = 0.9f;
    [SerializeField]
    float gainMax = 100f;
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
                float r = raidus * (1 - Mathf.Pow((float)i / row + skinWidth, 1 / 2f));
                float nextR = raidus * (1 - Mathf.Pow((float)(i + 1) / row - skinWidth, 1 / 2f));
                float theta = (360f * j / column + skinAngle) * Mathf.Deg2Rad;
                float nextTheta = (360f * (j + 1) / column - skinAngle) * Mathf.Deg2Rad;
                vertices[vIndex + 0] = new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta));
                vertices[vIndex + 1] = new Vector3(r * Mathf.Cos(nextTheta), r * Mathf.Sin(nextTheta));
                vertices[vIndex + 2] = new Vector3(nextR * Mathf.Cos(theta), nextR * Mathf.Sin(theta));
                vertices[vIndex + 3] = new Vector3(nextR * Mathf.Cos(nextTheta), nextR * Mathf.Sin(nextTheta));
                colors[vIndex + 0] = Color.black;
                colors[vIndex + 1] = Color.black;
                colors[vIndex + 2] = Color.black;
                colors[vIndex + 3] = Color.black;
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
        //audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        float[] spectrumSum = new float[column];
        for (int i = 0; i < resolution; i++)
        {
            float ratio = (float)i / resolution;
            spectrumSum[column * i / resolution] += spectrum[i] * Mathf.Exp(ratio * Mathf.Log(gainMax));
        }
        int cIndex = 0;
        for (int j = 0; j < column; j++)
        {
            for (int i = 0; i < row; i++)
            {
                if (spectrumSum[j] >= max * (i + 1) / row)
                {
                    colors[cIndex + 0] = Color.white;
                    colors[cIndex + 1] = Color.white;
                    colors[cIndex + 2] = Color.white;
                    colors[cIndex + 3] = Color.white;
                }
                else
                {
                    colors[cIndex + 0] *= attenuationRate;
                    colors[cIndex + 1] *= attenuationRate;
                    colors[cIndex + 2] *= attenuationRate;
                    colors[cIndex + 3] *= attenuationRate;
                }
                cIndex += 4;
            }
        }

        mesh.SetColors(colors);
        mf.mesh = mesh;
    }

    private void OnValidate()
    {
        GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
    }
}
