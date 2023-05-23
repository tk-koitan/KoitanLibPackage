using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KoitanLib
{
    public class ControllerInputHuman : MonoBehaviour, IControllerInput
    {
        private PlayerInput playerInput;


        /// <summary>
        /// アナログ入力のデッドライン
        /// </summary>
        private float deadline = 0.1f;
        private Dictionary<ButtonCode, InputAction> currentButtonInput = new Dictionary<ButtonCode, InputAction>();
        private Dictionary<ButtonCode, bool> button = new Dictionary<ButtonCode, bool>();
        private Dictionary<ButtonCode, bool> oldButton = new Dictionary<ButtonCode, bool>();
        private Dictionary<StickCode, InputAction> currentStickInput = new Dictionary<StickCode, InputAction>();
        private Dictionary<StickCode, Vector2> stick = new Dictionary<StickCode, Vector2>();
        private string controllerName = "ControllerName";
        private int joinIndex = -1;

        // Start is called before the first frame update
        void Awake()
        {
            TryGetComponent(out playerInput);
            for (int i = 0; i < KoitanInput.buttonCodes.Length; i++)
            {
                ButtonCode code = KoitanInput.buttonCodes[i];
                //ButtonCodeとActionの名前を一致させる
                button.Add(code, false);
                oldButton.Add(code, false);
                currentButtonInput.Add(code, playerInput.currentActionMap.FindAction(code.ToString()));
            }
            for (int i = 0; i < KoitanInput.stickCodes.Length; i++)
            {
                StickCode code = KoitanInput.stickCodes[i];
                //StickCodeとActionの名前を一致させる
                currentStickInput.Add(code, playerInput.currentActionMap.FindAction(code.ToString()));
                stick.Add(code, Vector2.zero);
            }
            DontDestroyOnLoad(this);
            controllerName = playerInput.devices[0].name;
            //登録
            joinIndex = KoitanInput.JoinController(this);
        }


        public void UpdateInput()
        {
            //更新
            for (int i = 0; i < KoitanInput.buttonCodes.Length; i++)
            {
                ButtonCode code = KoitanInput.buttonCodes[i];
                oldButton[code] = button[code];
                button[code] = currentButtonInput[code].ReadValue<float>() > deadline;
            }

            for (int i = 0; i < KoitanInput.stickCodes.Length; i++)
            {
                StickCode code = KoitanInput.stickCodes[i];
                stick[code] = currentStickInput[code].ReadValue<Vector2>();
            }
        }

        public bool Get(ButtonCode code)
        {
            return button[code];
        }

        public bool GetDown(ButtonCode code)
        {
            return !oldButton[code] && button[code];
        }
        public bool GetUp(ButtonCode code)
        {
            return oldButton[code] && !button[code];
        }

        public Vector2 GetStick(StickCode code)
        {
            Vector2 input = stick[code];
            if (input.sqrMagnitude < deadline * deadline)
            {
                return Vector2.zero;
            }
            return input;
        }

        public string GetControllerName()
        {
            return controllerName;
        }

        public int GetIndex()
        {
            return joinIndex;
        }

        public void DeleteSelf()
        {
            Destroy(gameObject);
        }
    }

}
