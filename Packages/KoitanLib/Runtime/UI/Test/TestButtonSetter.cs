using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    public class TestButtonSetter : MonoBehaviour
    {
        [SerializeField]
        TestButton testButton;
        // Start is called before the first frame update
        void Start()
        {
            testButton.onClickCallBack = () =>
            {
                Debug.Log("push");
            };
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
