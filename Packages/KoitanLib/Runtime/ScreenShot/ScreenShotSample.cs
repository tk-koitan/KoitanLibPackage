using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    public class ScreenShotSample : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F12))
            {
                ScreenShot.CaptureScreenshot();
            }
        }
    }
}
