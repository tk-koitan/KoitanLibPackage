using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KoitanLib
{
    public interface IControllerInput
    {
        /// <summary>
        /// Initializeの前に呼ばれる処理
        /// </summary>
        public void BeforeInitialize();
        /// <summary>
        /// コントローラーを登録するときに呼ばれる処理
        /// </summary>
        public void Initialize();
        /// <summary>
        /// 毎フレームUpdateInputの前に状態を保存するために呼ばれる処理
        /// </summary>
        public void BeforeUpdateInput();
        /// <summary>
        /// 毎フレーム入力を更新するために呼ばれる処理
        /// </summary>
        public void UpdateInput();
        /// <summary>
        /// ボタンを押しているか
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Get(ButtonCode code);
        /// <summary>
        /// ボタンを押した瞬間か
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool GetDown(ButtonCode code);
        /// <summary>
        /// ボタンを離した瞬間か
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool GetUp(ButtonCode code);
        /// <summary>
        /// ボタンの生の値
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public float GetRaw(ButtonCode code);
        /// <summary>
        /// スティックの入力量 (-1.0~1.0)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Vector2 GetStick(StickCode code);
        public Vector2 GetStickRaw(StickCode code);
        /// <summary>
        /// コントローラーの名前
        /// </summary>
        /// <returns></returns>
        public string GetControllerName();
        //public int GetIndex();
        public void SetMotorSpeeds(float low, float high, float duration);
        public void DeleteSelf();
    }
}

