using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KoitanLib
{
    public abstract class ControllerInput : MonoBehaviour, IControllerInput
    {
        protected float deadline = 0.1f;
        protected Dictionary<ButtonCode, bool> button = new Dictionary<ButtonCode, bool>();
        protected Dictionary<ButtonCode, float> buttonValue = new Dictionary<ButtonCode, float>();
        protected Dictionary<StickCode, Vector2> stick = new Dictionary<StickCode, Vector2>();
        protected Dictionary<ButtonCode, bool> oldButton = new Dictionary<ButtonCode, bool>();
        protected string controllerName = "ControllerName";
        protected int joinIndex = -1;

        public void BeforeInitialize()
        {
            for (int i = 0; i < KoitanInput.buttonCodes.Length; i++)
            {
                ButtonCode code = KoitanInput.buttonCodes[i];
                button.Add(code, false);
                buttonValue.Add(code, 0f);
                oldButton.Add(code, false);
            }
            for (int i = 0; i < KoitanInput.stickCodes.Length; i++)
            {
                StickCode code = KoitanInput.stickCodes[i];
                stick.Add(code, Vector2.zero);
            }
        }


        public virtual void Initialize()
        {

        }

        public void BeforeUpdateInput()
        {
            //‘O‚Ìî•ñ‚ðŠo‚¦‚Ä‚¨‚­
            for (int i = 0; i < KoitanInput.buttonCodes.Length; i++)
            {
                ButtonCode code = KoitanInput.buttonCodes[i];
                oldButton[code] = button[code];
            }

            for (int i = 0; i < KoitanInput.stickCodes.Length; i++)
            {
                StickCode code = KoitanInput.stickCodes[i];
            }
        }



        public virtual void UpdateInput()
        {

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

        public float GetRaw(ButtonCode code)
        {
            return buttonValue[code];
        }

        public void SetButtonValue(ButtonCode code, float value)
        {
            buttonValue[code] = value;
            button[code] = value > deadline;
        }

        public void SetButtonValue(ButtonCode code, bool value)
        {
            buttonValue[code] = value ? 1f : 0f;
            button[code] = value;
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

