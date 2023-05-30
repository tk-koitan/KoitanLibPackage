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

        // Update is called once per frame
        void Update()
        {
            transform.localScale = Vector3.one * (KoitanInput.GetRaw(code, playerNum) * 0.5f + 1f);
        }
    }
}
