using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KoitanLib
{
    [DefaultExecutionOrder(-99)]
    public class KoitanInput : MonoBehaviour
    {
        public static KoitanInput Instance { get; private set; }
        List<IControllerInput> controllerList = new List<IControllerInput>();
        public static ButtonCode[] buttonCodes;
        public static StickCode[] stickCodes;
        public static bool InUpdateLoop { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(this);
                Instance = this;
                // foreachを使いたくないのでButtonCodeの配列を作る、もっといい方法がある気がする
                buttonCodes = new ButtonCode[Enum.GetValues(typeof(ButtonCode)).Length];
                Enum.GetValues(typeof(ButtonCode)).CopyTo(buttonCodes, 0);
                stickCodes = new StickCode[Enum.GetValues(typeof(StickCode)).Length];
                Enum.GetValues(typeof(StickCode)).CopyTo(stickCodes, 0);
                //Debug.Log(string.Join(",", buttonCodes));
                InUpdateLoop = false;
            }
            else
            {
                // PlayerManagerごと破棄                
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            InUpdateLoop = false;
            InputSystem.Update();
            // コントローラーの更新
            for (int i = 0; i < controllerList.Count; i++)
            {
                controllerList[i].BeforeUpdateInput();
                controllerList[i].UpdateInput();
            }
        }

        // Update is called once per frame
        void Update()
        {
            InUpdateLoop = true;
            InputSystem.Update();            
            // コントローラーの更新
            for (int i = 0; i < controllerList.Count; i++)
            {
                controllerList[i].BeforeUpdateInput();
                controllerList[i].UpdateInput();
            }

            string str = string.Empty;
            str += "Name:ABXYLBRBLTRTLPRP\n";
            for (int i = 0; i < controllerList.Count; i++)
            {

                str += $"{controllerList[i].GetControllerName()}:" +
                    $"{(controllerList[i].Get(ButtonCode.A) ? "1" : "0")}" +
                    $"{(controllerList[i].Get(ButtonCode.B) ? "1" : "0")}" +
                    $"{(controllerList[i].Get(ButtonCode.X) ? "1" : "0")}" +
                    $"{(controllerList[i].Get(ButtonCode.Y) ? "1" : "0")}" +
                    $"{(controllerList[i].Get(ButtonCode.LB) ? "1" : "0")}" +
                    $"{(controllerList[i].Get(ButtonCode.RB) ? "1" : "0")}" +
                    $"{(controllerList[i].Get(ButtonCode.LT) ? "1" : "0")}" +
                    $"{(controllerList[i].Get(ButtonCode.RT) ? "1" : "0")}" +
                    $"{(controllerList[i].Get(ButtonCode.LeftStickPress) ? "1" : "0")}" +
                    $"{(controllerList[i].Get(ButtonCode.RightStickPress) ? "1" : "0")}" +
                    $"{controllerList[i].GetStick(StickCode.LeftStick)}" +
                    $"{controllerList[i].GetStick(StickCode.RightStick)}" +
                    $"{controllerList[i].GetStick(StickCode.DPad)}" +
                    $"\n";

                /*
                str += $"{controllerList[i].GetControllerName()}:" +
                    $"{(controllerList[i].GetRaw(ButtonCode.A))}" +
                    $"{(controllerList[i].GetRaw(ButtonCode.LT))}" +
                    $"\n";
                */
            }
            //str += $"Gamepad.current.name = {Gamepad.current.name}\n";
            /*
            foreach (var gamepad in Gamepad.all)
            {
                str += $"{gamepad.name}:{gamepad.aButton.value}\n";
            }
            */
            KoitanDebug.Display(str);
            /*
            if (KoitanInput.GetDown(ButtonCode.B))
            {
                Debug.Log("KoitanB");
            }
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse");
            }
            */
            /*
            if (KoitanInput.GetDown(ButtonCode.LeftStickPress))
            {
                DeleteAllControllers();
            }
            */
        }

        /// <summary>
        /// ボタンの生の値
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static float GetRaw(ButtonCode code, int index)
        {
            return index < Instance.controllerList.Count ? Instance.controllerList[index].GetRaw(code) : 0f;
        }

        /// <summary>
        /// ボタンを押しているか
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool Get(ButtonCode code, int index)
        {
            return index < Instance.controllerList.Count ? Instance.controllerList[index].Get(code) : false;
        }

        public static bool Get(ButtonCode code)
        {
            foreach (var controller in Instance.controllerList)
            {
                if (controller.Get(code))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ボタンを押した瞬間か
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetDown(ButtonCode code, int index)
        {
            return index < Instance.controllerList.Count ? Instance.controllerList[index].GetDown(code) : false;
        }

        public static bool GetDown(ButtonCode code)
        {
            foreach (var controller in Instance.controllerList)
            {
                if (controller.GetDown(code))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ボタンを離した瞬間か
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetUp(ButtonCode code, int index)
        {
            return index < Instance.controllerList.Count ? Instance.controllerList[index].GetUp(code) : false;
        }

        public static bool GetUp(ButtonCode code)
        {
            foreach (var controller in Instance.controllerList)
            {
                if (controller.GetUp(code))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// スティックの入力量 (-1.0~1.0)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Vector2 GetStick(StickCode code, int index)
        {
            return index < Instance.controllerList.Count ? Instance.controllerList[index].GetStick(code) : Vector2.zero;
        }

        public static Vector2 GetStick(StickCode code)
        {
            // 最大の大きさのスティックの値を取得する
            float maxDistance = 0f;
            Vector2 maxInput = Vector2.zero;
            foreach (var controller in Instance.controllerList)
            {
                Vector2 input = controller.GetStick(code);
                if (input.sqrMagnitude > maxDistance)
                {
                    maxDistance = input.sqrMagnitude;
                    maxInput = input;
                }
            }
            return maxInput;
        }

        /// <summary>
        /// コントローラーの名前
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetControllerName(int index)
        {
            return index < Instance.controllerList.Count ? Instance.controllerList[index].GetControllerName() : "None";
        }

        public static void SetMoterSpeeds(float low, float high, float duration)
        {
            foreach (var controller in Instance.controllerList)
            {
                controller.SetMotorSpeeds(low, high, duration);
            }
        }

        /// <summary>
        /// 振動させる
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="duration"></param>
        /// <param name="index"></param>
        public static void SetMoterSpeeds(float low, float high, float duration, int index)
        {
            if (index < Instance.controllerList.Count)
            {
                Instance.controllerList[index].SetMotorSpeeds(low, high, duration);
            }
        }

        public static int JoinController(IControllerInput controllerInput)
        {
            Instance.controllerList.Add(controllerInput);
            controllerInput.BeforeInitialize();
            controllerInput.Initialize();
            return Instance.controllerList.Count - 1;
        }

        public static void DeleteAllControllers()
        {
            for (int i = 0; i < Instance.controllerList.Count; i++)
            {
                Instance.controllerList[i].DeleteSelf();
            }
            Instance.controllerList.Clear();
        }

        public void OnPlayerJoined(PlayerInput playerInput)
        {

            for (int i = 0; i < playerInput.devices.Count; i++)
            {
                Debug.Log($"{i}:{playerInput.devices[i]}");
            }

            JoinController(playerInput.GetComponent<IControllerInput>());
        }
    }

    public enum ButtonCode
    {
        A,
        B,
        X,
        Y,
        LB,
        RB,
        LT,
        RT,
        LeftStickPress,
        RightStickPress,
        Start,
        Select,
        Up,
        Down,
        Right,
        Left,
    }

    public enum StickCode
    {
        LeftStick,
        RightStick,
        DPad,
    }
}

