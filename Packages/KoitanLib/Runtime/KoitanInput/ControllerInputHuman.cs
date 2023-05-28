using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KoitanLib
{
    public class ControllerInputHuman : ControllerInput
    {
        private PlayerInput playerInput;
        private Dictionary<ButtonCode, InputAction> currentButtonInput = new Dictionary<ButtonCode, InputAction>();
        private Dictionary<StickCode, InputAction> currentStickInput = new Dictionary<StickCode, InputAction>();

        public override void Initialize()
        {
            TryGetComponent(out playerInput);
            for (int i = 0; i < KoitanInput.buttonCodes.Length; i++)
            {
                ButtonCode code = KoitanInput.buttonCodes[i];
                currentButtonInput.Add(code, playerInput.currentActionMap.FindAction(code.ToString()));
            }
            for (int i = 0; i < KoitanInput.stickCodes.Length; i++)
            {
                StickCode code = KoitanInput.stickCodes[i];
                currentStickInput.Add(code, playerInput.currentActionMap.FindAction(code.ToString()));
            }
            DontDestroyOnLoad(this);
            controllerName = playerInput.devices[0].name;
        }


        public override void UpdateInput()
        {
            //XV
            for (int i = 0; i < KoitanInput.buttonCodes.Length; i++)
            {
                ButtonCode code = KoitanInput.buttonCodes[i];
                SetButtonValue(code, currentButtonInput[code].ReadValue<float>());
            }

            for (int i = 0; i < KoitanInput.stickCodes.Length; i++)
            {
                StickCode code = KoitanInput.stickCodes[i];
                stick[code] = currentStickInput[code].ReadValue<Vector2>();
            }
        }
    }

}
