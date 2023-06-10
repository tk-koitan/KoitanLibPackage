using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public static class DebugSceneAdder
{
#if UNITY_2021_3_OR_NEWER
    [MenuItem("KoitanLib/AddScene")]
    private static void AddScene()
    {
        var scenes = EditorBuildSettings.scenes;
        string debugScenePath = "Packages/com.koitan.koitanlib/KoitanLib/Scenes/DebugScene.unity";
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].path == debugScenePath)
            {
                Debug.Log("既にデバッグシーンがあります");
                return;
            }
        }
        ArrayUtility.Add(ref scenes, new EditorBuildSettingsScene(debugScenePath, true));
        EditorBuildSettings.scenes = scenes;
        Debug.Log("デバッグシーンを追加しました");
    }

    [MenuItem("KoitanLib/RemoveScene")]
    private static void RemoveScene()
    {
        var scenes = EditorBuildSettings.scenes;
        string debugScenePath = "Packages/com.koitan.koitanlib/KoitanLib/Scenes/DebugScene.unity";
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].path == debugScenePath)
            {
                ArrayUtility.RemoveAt(ref scenes, i);
                EditorBuildSettings.scenes = scenes;
                Debug.Log("デバッグシーンを消しました");
                return;
            }
        }
        Debug.Log("デバッグシーンがありません");
    }

    [MenuItem("KoitanLib/AddDefine")]
    private static void AddDefine()
    {
        string debugDefine = "KOITAN_DEBUG";
        var sbtg = EditorUserBuildSettings.selectedBuildTargetGroup;
        string[] defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(sbtg).Split(";");
        for (int i = 0; i < defines.Length; i++)
        {
            if (defines[i] == debugDefine)
            {
                Debug.Log("既にKOITAN＿DEBUGはあります");
                return;
            }
        }

        ArrayUtility.Add(ref defines, debugDefine);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(sbtg, string.Join(";", defines));
        Debug.Log("KOITAN_DEBUGを追加しました");
    }

    [MenuItem("KoitanLib/RemoveDefine")]
    private static void RemoveDefine()
    {
        string debugDefine = "KOITAN_DEBUG";
        var sbtg = EditorUserBuildSettings.selectedBuildTargetGroup;
        string[] defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(sbtg).Split(";");
        for (int i = 0; i < defines.Length; i++)
        {
            if (defines[i] == debugDefine)
            {
                ArrayUtility.RemoveAt(ref defines, i);
                Debug.Log("KOITAN＿DEBUGを削除しました");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(sbtg, string.Join(";", defines));
                return;
            }
        }
        Debug.Log("KOITAN_DEBUGが見つかりません");
    }
#endif    
}
