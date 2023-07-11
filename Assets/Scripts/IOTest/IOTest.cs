using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using DG.Tweening.Plugins.Core.PathCore;

namespace KoitanLib
{
    public class IOTest : MonoBehaviour
    {
        string path = $"{Application.streamingAssetsPath}/TestData";
        float[,] data = new float[4000, 32];
        // Start is called before the first frame update
        void Start()
        {
            //ƒ‰ƒ“ƒ_ƒ€‚È”š‚Å–„‚ß‚é
            for (int j = 0; j < data.GetLength(0); j++)
            {
                for (int i = 0; i < data.GetLength(1); i++)
                {
                    data[j, i] = UnityEngine.Random.Range(0f, 1f);
                }
            }
            using (FileStream fs = new FileStream(path, FileMode.Create))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    for (int i = 0; i < data.GetLength(1); i++)
                    {
                        bw.Write(data[j, i]);
                    }
                }
                /*
                bw.Write(13);
                bw.Write("testaaaa0000");
                bw.Write("aiueo");
                bw.Write(true);
                */
            }

            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                //Debug.Log(br.ReadInt32());
                /*
                Debug.Log(br.ReadByte());
                Debug.Log(br.ReadByte());
                Debug.Log(br.ReadByte());
                Debug.Log(br.ReadByte());
                Debug.Log(br.ReadString());
                Debug.Log(br.ReadString());
                */
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    for (int i = 0; i < data.GetLength(1); i++)
                    {
                        Debug.Log(br.ReadSingle());
                    }
                }
            }

            /*
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                byte[] buffer = new byte[1024];
                fs.Read(buffer, 0, buffer.Length);
                Debug.Log(string.Join(", ", buffer));
            }

            using(FileStream fs = new FileStream(filePath, FileMode.Create))
            {

            }
            */
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

