using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

namespace KoitanLib.Editor
{
    public class ScreenShotEditor
    {
        // �����^�C���ƃG�f�B�^�[�łقړ����R�[�h
        // ctrl(mac�Ȃ�command)+alt+S���V���[�g�J�b�g�ɐݒ�
        [MenuItem("KoitanLib/Screenshot %&S", false)]
        public static void CaptureScreenshot()
        {
            var FilePath = Application.dataPath;//Editor��ł͕��ʂɃJ�����g�f�B���N�g�����m�F
            if (!Directory.Exists($"{FilePath}/Screenshots"))
            {
                Directory.CreateDirectory($"{FilePath}/Screenshots");
            }
            string fileName = $"{FilePath}/Screenshots/{DateTime.Now.ToString().Replace('/', '_').Replace(':', '_')}.png";
            ScreenCapture.CaptureScreenshot(fileName);
            Debug.Log($"{fileName}");
        }
    }
}
