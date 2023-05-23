using KoitanLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoitanInputAllClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (KoitanInput.GetDown(ButtonCode.Start))
        {
            KoitanInput.DeleteAllControllers();
        }
    }
}
