using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    public class KoitanInputSimpleButton : MonoBehaviour
    {
        [SerializeField]
        int playerNum;
        [SerializeField]
        ButtonCode code;
        [SerializeField, Range(0f, 1f)]
        float low, high;

        // Update is called once per frame
        void Update()
        {
            transform.localScale = Vector3.one * (KoitanInput.GetRaw(code, playerNum) * 0.5f + 1f);
            if (KoitanInput.GetDown(code, playerNum))
            {
                KoitanInput.SetMoterSpeeds(low, high, 1f, playerNum);
            }
        }
    }
}
