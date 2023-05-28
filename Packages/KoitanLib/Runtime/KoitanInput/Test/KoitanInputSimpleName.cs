using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using KoitanLib;

public class KoitanInputSimpleName : MonoBehaviour
{
    TMP_Text textMesh;
    [SerializeField]
    int playerNum;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out textMesh);
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = KoitanInput.GetControllerName(playerNum);
    }
}
