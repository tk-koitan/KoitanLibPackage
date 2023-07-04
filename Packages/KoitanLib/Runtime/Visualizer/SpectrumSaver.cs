using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace KoitanLib
{
    public class SpectrumSaver : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioSpectrumData data;
        int resolution = 1024;
        [SerializeField]
        int column = 6;
        [SerializeField]
        float gainMax = 100f;
        int cnt = 0;
#if UNITY_EDITOR
        // Start is called before the first frame update
        void Start()
        {
            //data = ScriptableObject.CreateInstance<AudioSpectrumData>();
            float l = audioSource.clip.length;
            int n = (int)(l / Time.fixedDeltaTime);
            data.fixedDeltaTime = Time.fixedDeltaTime;
            data.datas = new AudioSpectrumSum[n];
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // spectrum計算
            float[] spectrum = new float[resolution];
            audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
            float[] spectrumSum = new float[column];
            for (int i = 0; i < resolution; i++)
            {
                float ratio = (float)i / resolution;
                spectrumSum[column * i / resolution] += spectrum[i] * Mathf.Exp(ratio * Mathf.Log(gainMax));
            }
            if (cnt < data.datas.GetLength(0))
            {
                // データ書き込み
                data.datas[cnt].sum = spectrumSum;
                /*
                for (int i = 0; i < column; i++)
                {
                    data.datas[i].sum[i] = spectrumSum[i];
                }
                */
            }
            if (cnt == data.datas.GetLength(0))
            {
                // 保存して終了
                EditorUtility.SetDirty(data);
                Debug.Log("保存");
                /*
                string filePath = Application.dataPath + "/spectrum";
                AssetDatabase.CreateAsset(data, filePath);
                AssetDatabase.ImportAsset(filePath);
                */
                /*
                string json = JsonUtility.ToJson(spectrum, true);
                StreamWriter streamWriter = new StreamWriter(filePath);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
                Debug.Log($"{filePath}に保存しました");
                */

            }
            cnt++;
        }
#endif
    }

}
