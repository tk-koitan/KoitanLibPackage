using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KoitanLib
{
    public class ControllerInputAI : ControllerInput
    {
        private void Awake()
        {
            controllerName = "AI";
            KoitanInput.JoinController(this);
            DontDestroyOnLoad(this);
        }

        public override void Initialize()
        {
            //base.BeforeInitialize();
        }

        public override void UpdateInput()
        {
            //base.BeforeUpdateInput();
            stick[StickCode.LeftStick] = Quaternion.AngleAxis(Time.time * -180f, Vector3.forward) * Vector2.right;
        }
    }
}
