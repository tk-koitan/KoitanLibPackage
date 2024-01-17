using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using KoitanLib;

public class KoitanLibScript : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    string debugStr;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Welcom to KoitanLib\nHello, world2");
    }

    // Update is called once per frame
    void Update()
    {
        debugStr = string.Empty;
        // ゲームパッドが接続されていないとnullになる。
        if (Gamepad.current != null)
        {
            debugStr += $"Gamepad.current.buttonNorth.isPressed = {Gamepad.current.buttonNorth.isPressed}\n";
            debugStr += $"Gamepad.current.buttonSouth.isPressed = {Gamepad.current.buttonSouth.isPressed}\n";
        }
        KoitanDebug.Display($"debugStr = \n{debugStr}\n");
    }

    static void InitInstance()
    {

    }
}
