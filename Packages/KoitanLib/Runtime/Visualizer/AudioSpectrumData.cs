using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    [CreateAssetMenu(menuName = "ScriptableObject/SpectrumData")]
    public class AudioSpectrumData : ScriptableObject
    {
        public float fixedDeltaTime;
        public AudioSpectrumSum[] datas;
    }

    [Serializable]
    public struct AudioSpectrumSum
    {
        public float[] sum;
    }
}
