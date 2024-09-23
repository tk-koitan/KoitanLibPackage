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
        // ランタイムとエディターでほぼ同じコード
        // ctrl(macならcommand)+alt+Sをショートカットに設定
        [MenuItem("KoitanLib/Screenshot %&S", false)]
        public static void CaptureScreenshot()
        {
            var FilePath = Application.dataPath;//Editor上では普通にカレントディレクトリを確認
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
