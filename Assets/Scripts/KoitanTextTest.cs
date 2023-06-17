using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

public class KoitanTextTest : MonoBehaviour
{
    [SerializeField]
    int priority = 0;
    [SerializeField]
    string str;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        KoitanDebug.Display(str, priority);
    }
}
