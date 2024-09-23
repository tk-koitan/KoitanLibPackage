using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace KoitanLib
{
    public class ScreenShot
    {
        // ランタイムとエディターでほぼ同じコード
        public static void CaptureScreenshot()
        {
            var FilePath = Application.dataPath;//Editor上では普通にカレントディレクトリを確認
#if UNITY_EDITOR
            FilePath = Application.dataPath;//Editor上では普通にカレントディレクトリを確認
#elif UNITY_STANDALONE_WIN
            FilePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');//EXEを実行したカレントディレクトリ (ショートカット等でカレントディレクトリが変わるのでこの方式で)
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
