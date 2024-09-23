using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace KoitanLib
{
    public class ScreenShot
    {
        // �����^�C���ƃG�f�B�^�[�łقړ����R�[�h
        public static void CaptureScreenshot()
        {
            var FilePath = Application.dataPath;//Editor��ł͕��ʂɃJ�����g�f�B���N�g�����m�F
#if UNITY_EDITOR
            FilePath = Application.dataPath;//Editor��ł͕��ʂɃJ�����g�f�B���N�g�����m�F
#elif UNITY_STANDALONE_WIN
            FilePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');//EXE�����s�����J�����g�f�B���N�g�� (�V���[�g�J�b�g���ŃJ�����g�f�B���N�g�����ς��̂ł��̕�����)
#endif
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
