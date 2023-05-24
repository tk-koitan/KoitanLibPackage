using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    public class ControllerInputTouch : ControllerInput
    {
        private void Awake()
        {
            controllerName = "TouchPad";
            KoitanInput.JoinController(this);
        }

        public override void UpdateInput()
        {
            BeforeUpdateInput();
            Vector2 input = Input.mousePosition;
            input.x = (input.x / Screen.width) * 2 - 1f;
            input.y = (input.y / Screen.height) * 2 - 1f;
            stick[StickCode.LeftStick] = input;
        }
    }
}
