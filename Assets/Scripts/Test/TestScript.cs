using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (KoitanInput.GetDown(ButtonCode.A))
        {
            Debug.Log("A");
        }
    }
}
