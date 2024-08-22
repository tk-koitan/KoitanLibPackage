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
        protected Dictionary<ButtonCode, bool> buttonF = new Dictionary<ButtonCode, bool>();
        protected Dictionary<ButtonCode, float> buttonRaw = new Dictionary<ButtonCode, float>();
        protected Dictionary<ButtonCode, float> buttonRawF = new Dictionary<ButtonCode, float>();
        protected Dictionary<StickCode, Vector2> stick = new Dictionary<StickCode, Vector2>();
        protected Dictionary<StickCode, Vector2> stickF = new Dictionary<StickCode, Vector2>();
        protected Dictionary<StickCode, Vector2> stickRaw = new Dictionary<StickCode, Vector2>();
        protected Dictionary<StickCode, Vector2> stickRawF = new Dictionary<StickCode, Vector2>();
        protected Dictionary<ButtonCode, bool> oldButton = new Dictionary<ButtonCode, bool>();
        protected Dictionary<ButtonCode, bool> oldButtonF = new Dictionary<ButtonCode, bool>();
        protected string controllerName = "ControllerName";
        protected int joinIndex = -1;

        void Update()
        {
            //KoitanDebug.DisplayBox($"こんにちは世界{oldButtonF[ButtonCode.A]}", this);
        }

        public void BeforeInitialize()
        {
            for (int i = 0; i < KoitanInput.buttonCodes.Length; i++)
            {
                ButtonCode code = KoitanInput.buttonCodes[i];
                button.Add(code, false);
                buttonF.Add(code, false);
                buttonRaw.Add(code, 0f);
                buttonRawF.Add(code, 0f);
                oldButton.Add(code, false);
                oldButtonF.Add(code, false);
            }
            for (int i = 0; i < KoitanInput.stickCodes.Length; i++)
            {
                StickCode code = KoitanInput.stickCodes[i];
                stick.Add(code, Vector2.zero);
                stickF.Add(code, Vector2.zero);
            }
        }


        public virtual void Initialize()
        {

        }

        public void BeforeUpdateInput()
        {
            //前の情報を覚えておく
            for (int i = 0; i < KoitanInput.buttonCodes.Length; i++)
            {
                ButtonCode code = KoitanInput.buttonCodes[i];
                if (KoitanInput.InUpdateLoop)
                {
                    oldButton[code] = button[code];
                }
                else
                {
                    oldButtonF[code] = buttonF[code];
                }
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
            if (KoitanInput.InUpdateLoop)
            {
                return button[code];
            }
            else
            {
                return buttonF[code];
            }
        }

        public bool GetDown(ButtonCode code)
        {
            if (KoitanInput.InUpdateLoop)
            {
                return !oldButton[code] && button[code];
            }
            else
            {
                return !oldButtonF[code] && buttonF[code];
            }
        }
        public bool GetUp(ButtonCode code)
        {
            if (KoitanInput.InUpdateLoop)
            {
                return oldButton[code] && !button[code];
            }
            else
            {
                return oldButtonF[code] && !buttonF[code];
            }
        }

        public float GetRaw(ButtonCode code)
        {
            if (KoitanInput.InUpdateLoop)
            {
                return buttonRaw[code];
            }
            else
            {
                return buttonRawF[code];
            }
        }

        public void SetButtonValue(ButtonCode code, float value)
        {
            if (KoitanInput.InUpdateLoop)
            {
                buttonRaw[code] = value;
                // ここで閾値で分ける
                button[code] = value > deadline;
            }
            else
            {
                buttonRawF[code] = value;
                // ここで閾値で分ける
                buttonF[code] = value > deadline;
            }
        }

        public void SetButtonValue(ButtonCode code, bool value)
        {
            if (KoitanInput.InUpdateLoop)
            {
                buttonRaw[code] = value ? 1f : 0f;
                button[code] = value;
            }
            else
            {
                buttonRawF[code] = value ? 1f : 0f;
                buttonF[code] = value;
            }
        }

        public Vector2 GetStick(StickCode code)
        {
            Vector2 input;
            if (KoitanInput.InUpdateLoop)
            {
                input = stick[code];
            }
            else
            {
                input = stickF[code];
            }
            if (input.sqrMagnitude <= deadline * deadline)
            {
                return Vector2.zero;
            }
            return input;
        }

        public Vector2 GetStickRaw(StickCode code)
        {
            if (KoitanInput.InUpdateLoop)
            {
                return stickRaw[code];
            }
            else
            {
                return stickRawF[code];
            }
        }

        public void SetStickValue(StickCode code, Vector2 value)
        {
            if (KoitanInput.InUpdateLoop)
            {
                stickRaw[code] = value;
                if (value.sqrMagnitude <= deadline * deadline)
                {
                    stick[code] = Vector2.zero;
                }
                else
                {
                    stick[code] = value;
                }
            }
            else
            {
                stickRawF[code] = value;
                if (value.sqrMagnitude <= deadline * deadline)
                {
                    stickF[code] = Vector2.zero;
                }
                else
                {
                    stickF[code] = value;
                }
            }
        }

        public string GetControllerName()
        {
            return controllerName;
        }

        public virtual void SetMotorSpeeds(float low, float high, float duration)
        {

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

