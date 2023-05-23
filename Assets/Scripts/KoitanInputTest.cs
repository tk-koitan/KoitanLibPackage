using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using TMPro;

public class KoitanInputTest : MonoBehaviour
{
    [SerializeField]
    int playerNum;
    TMP_Text textMesh;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out textMesh);
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = $"{playerNum}:{KoitanInput.GetControllerName(playerNum)}";
        transform.Translate(KoitanInput.GetStick(StickCode.LeftStick, playerNum) * 10f * Time.deltaTime);
    }
}
