using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    public class SimpleVibration : MonoBehaviour
    {
        [SerializeField]
        int playerNum;
        [SerializeField]
        ButtonCode code;
        [SerializeField, Range(0f, 1f)]
        float low, high;
        [SerializeField]
        float duration;

        // Update is called once per frame
        void Update()
        {
            if (KoitanInput.GetDown(code, playerNum))
            {
                KoitanInput.SetMoterSpeeds(low, high, duration, playerNum);
            }
        }
    }
}
