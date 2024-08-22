using KoitanLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplayer : MonoBehaviour
{
    int fps = 0;
    int cnt = 0;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cnt++;
        timer += Time.unscaledDeltaTime;
        if (timer >= 1f)
        {
            timer -= 1f;
            fps = cnt;
            cnt = 0;
        }
        KoitanDebug.Display($"FPS = {fps}\n", -99);
    }
}
