using KoitanLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestUIInput : ControllerInput, IPointerDownHandler, IPointerUpHandler
{
    private void Awake()
    {
        controllerName = "UI";
        KoitanInput.JoinController(this);
        DontDestroyOnLoad(this);
    }

    public override void UpdateInput()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetButtonValue(ButtonCode.B, true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetButtonValue(ButtonCode.B, false);
    }
}
