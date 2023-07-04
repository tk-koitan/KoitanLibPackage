using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    [RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
    public class Visualizer : MonoBehaviour
    {
        MeshFilter mf;
        private Mesh mesh;
        private Vector3[] vertices = new Vector3[4];
        private Color[] colors = new Color[4];
        private int[] triangles = new int[6];
        private Vector2[] uvs = new Vector2[6];
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
        [SerializeField]
        float raidus = 1f;
        [SerializeField]
        float attenuationRate = 0.9f;
        [SerializeField]
        float gainMax = 100f;
        [SerializeField]
        AudioSpectrumData spectrumData;
        [SerializeField]
        bool isUseBakeData;
        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out mf);
            mesh = new Mesh();
            vertices = new Vector3[4 * row * column];
            triangles = new int[6 * row * column];
            colors = new Color[4 * row * column];
            uvs = new Vector2[4 * row * column];
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
                    colors[vIndex + 0] = Color.clear;
                    colors[vIndex + 1] = Color.clear;
                    colors[vIndex + 2] = Color.clear;
                    colors[vIndex + 3] = Color.clear;
                    uvs[vIndex + 0] = new Vector2(0f, 0f);
                    uvs[vIndex + 1] = new Vector2(1f, 0f);
                    uvs[vIndex + 2] = new Vector2(0f, 1f);
                    uvs[vIndex + 3] = new Vector2(1f, 1f);
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
            mesh.SetUVs(0, uvs);
            mf.mesh = mesh;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float[] spectrumSum = new float[column];
            // スペクトラム計算部分
            if (isUseBakeData)
            {
                int n = (int)(audioSource.time / spectrumData.fixedDeltaTime);
                if (n > 0 && n < spectrumData.datas.Length)
                {
                    //spectrumSum = spectrumData.datas[n].sum;
                    for (int i = 0; i < spectrumSum.Length; i++)
                    {
                        spectrumSum[i] = spectrumData.datas[n].sum[spectrumData.datas[n].sum.Length * i / column];
                    }
                }
            }
            else
            {
                float[] spectrum = new float[resolution];
                audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
                for (int i = 0; i < resolution; i++)
                {
                    float ratio = (float)i / resolution;
                    spectrumSum[column * i / resolution] += spectrum[i] * Mathf.Exp(ratio * Mathf.Log(gainMax));
                }
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
                        /*
                        colors[cIndex + 0] = Color.clear;
                        colors[cIndex + 1] = Color.clear;
                        colors[cIndex + 2] = Color.clear;
                        colors[cIndex + 3] = Color.clear;
                        */
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
    }
}

