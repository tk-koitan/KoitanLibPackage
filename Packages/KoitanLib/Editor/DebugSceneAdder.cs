using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public static class DebugSceneAdder
{
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
}
