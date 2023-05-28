using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KoitanLib
{
    public interface IControllerInput
    {
        /// <summary>
        /// Initialize�̑O�ɌĂ΂�鏈��
        /// </summary>
        public void BeforeInitialize();
        /// <summary>
        /// �R���g���[���[��o�^����Ƃ��ɌĂ΂�鏈��
        /// </summary>
        public void Initialize();
        /// <summary>
        /// ���t���[��UpdateInput�̑O�ɏ�Ԃ�ۑ����邽�߂ɌĂ΂�鏈��
        /// </summary>
        public void BeforeUpdateInput();
        /// <summary>
        /// ���t���[�����͂��X�V���邽�߂ɌĂ΂�鏈��
        /// </summary>
        public void UpdateInput();
        /// <summary>
        /// �{�^���������Ă��邩
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Get(ButtonCode code);
        /// <summary>
        /// �{�^�����������u�Ԃ�
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool GetDown(ButtonCode code);
        /// <summary>
        /// �{�^���𗣂����u�Ԃ�
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool GetUp(ButtonCode code);
        /// <summary>
        /// �{�^���̐��̒l
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public float GetRaw(ButtonCode code);
        /// <summary>
        /// �X�e�B�b�N�̓��͗� (-1.0~1.0)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Vector2 GetStick(StickCode code);
        public Vector2 GetStickRaw(StickCode code);
        /// <summary>
        /// �R���g���[���[�̖��O
        /// </summary>
        /// <returns></returns>
        public string GetControllerName();
        //public int GetIndex();
        public void SetMotorSpeeds(float low, float high, float duration);
        public void DeleteSelf();
    }
}

