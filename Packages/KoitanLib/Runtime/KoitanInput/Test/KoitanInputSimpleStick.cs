using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    public class KoitanInputSimpleStick : MonoBehaviour
    {
        [SerializeField]
        int playerNum;
        [SerializeField]
        StickCode code;

        private Vector3 startPos;
        private void Awake()
        {
            startPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = startPos + (Vector3)KoitanInput.GetStick(code, playerNum);
        }
    }
}
