using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KoitanLib
{
    public interface IControllerInput
    {
        public void UpdateInput();
        public bool Get(ButtonCode code);
        public bool GetDown(ButtonCode code);
        public bool GetUp(ButtonCode code);

        public Vector2 GetStick(StickCode code);

        public string GetControllerName();
        public int GetIndex();
        public void DeleteSelf();
    }
}

