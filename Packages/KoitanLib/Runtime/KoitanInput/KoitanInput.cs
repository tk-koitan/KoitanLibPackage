using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    [DefaultExecutionOrder(-99)]
    public class KoitanInput : MonoBehaviour
    {
        public static KoitanInput Instance { get; private set; }
        List<IControllerInput> controllerList = new List<IControllerInput>();
        public static ButtonCode[] buttonCodes;
        public static StickCode[] stickCodes;

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
            }
            else
            {
                Destroy(this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // コントローラーの更新
            for (int i = 0; i < controllerList.Count; i++)
            {
                controllerList[i].UpdateInput();
            }
            /*
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
            }
            KoitanDebug.Display(str);
            */
            /*
            if (KoitanInput.GetDown(ButtonCode.LeftStickPress))
            {
                DeleteAllControllers();
            }
            */
        }

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

        public static string GetControllerName(int index)
        {
            return index < Instance.controllerList.Count ? Instance.controllerList[index].GetControllerName() : "None";
        }

        public static int JoinController(IControllerInput controllerInput)
        {
            Instance.controllerList.Add(controllerInput);
            return Instance.controllerList.Count - 1;
        }

        public static void DeleteAllControllers()
        {
            for (int i = 0; i < Instance.controllerList.Count; i++)
            {
                Destroy(Instance.controllerList[i].GetPlayerInput().gameObject);
            }
            Instance.controllerList.Clear();
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

