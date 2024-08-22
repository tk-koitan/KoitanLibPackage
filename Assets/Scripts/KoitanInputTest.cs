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
    int countNewF = 0;
    int countOldF = 0;
    int countNew = 0;
    int countOld = 0;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out textMesh);
    }

    // Update is called once per frame
    void Update()
    {
        if (KoitanInput.GetDown(ButtonCode.A))
        {
            countNew++;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            countOld++;
        }

        textMesh.text = $"{playerNum}:{KoitanInput.GetControllerName(playerNum)}\ncountOld = {countOld}\ncountOldF = {countOldF}\ncountNew = {countNew}\ncountNewF = {countNewF}";
    }

    private void FixedUpdate()
    {
        transform.Translate(KoitanInput.GetStick(StickCode.LeftStick, playerNum) * 10f * Time.deltaTime);
        if (KoitanInput.GetDown(ButtonCode.A))
        {
            countNewF++;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            countOldF++;
        }
    }
}
